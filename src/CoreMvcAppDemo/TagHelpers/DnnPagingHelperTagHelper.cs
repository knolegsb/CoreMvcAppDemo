using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.TagHelpers
{
    /// <summary>
    /// 페이징 헬퍼(dnn-paging-helper)
    /// </summary>
    public class DnnPagingHelperTagHelper : TagHelper
    {
        public bool SearchMode { get; set; } = false;
        public string SearchField { get; set; }
        public string SearchQuery { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageCount { get; set; }
        public int PageSize { get; set; } = 10;
        public string Url { get; set; }

        private int _RecordCount;
        public int RecordCount
        {
            get { return _RecordCount; }
            set
            {
                _RecordCount = value;
                PageCount = ((_RecordCount - 1) / PageSize) + 1;
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination pagination-sm");

            if (PageIndex == 0)
            {
                PageIndex = 1;
            }

            int i = 0;
            string strPage = "";

            if (PageIndex > 10)
            {
                if (!SearchMode)
                {
                    strPage += "<li><a href=\"" + Url + "?Page=" + Convert.ToString(((PageIndex - 1) / (int)10) * 10) +"\">◀</a></li>";
                }
                else
                {
                    strPage += "<li><a href=\"" + Url + "?Page=" + Convert.ToString(((PageIndex - 1) / (int)10) * 10) + "&SearchField=" + SearchField + "&SearchQuery=" + SearchQuery + "\">◀</a></li>";
                }
            }
            else
            {
                strPage += "<li class='disabled'><a>◁</a></li>"; 
            }

            for (i = (((PageIndex - 1) / (int)10) * 10 + 1); i <= ((((PageIndex - 1) / (int)10) + 1) * 10) ; i++)
            {
                if (i > PageCount)
                {
                    break;
                }
                if (i == PageIndex)
                {
                    strPage += " <li class='active'><a href='#'>" + i.ToString() + "</a></li>";
                }
                else
                {
                    if (!SearchMode)
                    {
                        strPage += "<li><a href=\"" + Url + "?Page=" + i.ToString() + "\">" + i.ToString() + "</a></li>";
                    }
                    else
                    {
                        strPage += "<li><a href=\"" + Url + "?Page=" + i.ToString() + "&SearchField=" + SearchField + "&SearchQuery=" + SearchQuery + "\">" + i.ToString() + "</a></li>";
                    }
                }
            }

            if (i < PageCount)
            {
                if (!SearchMode)
                {
                    if (!SearchMode)
                    {
                        strPage += "<li><a href=\\" + Url + "?Page=" + Convert.ToString(((PageIndex - 1) / (int)10) * 10 + 11) + "\">▶</a></li>";
                    }
                    else
                    {
                        strPage += "<li><a href=\"" + Url + "?Page=" + Convert.ToString(((PageIndex - 1) / (int)10) * 10 + 11) + "&SearchField=" + SearchField + "&SearchQuery=" + SearchQuery + "\">▶</a></li>";
                    }
                }
                else
                {
                    strPage += "<li class='disabled'><a>▷</a></li>";
                }

                output.Content.AppendHtml(strPage);
            }
        }
    }
}

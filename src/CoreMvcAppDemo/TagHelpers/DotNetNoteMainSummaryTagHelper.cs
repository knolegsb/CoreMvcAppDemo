﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using CoreMvcAppDemo.Models;

namespace CoreMvcAppDemo.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("tag-name")]
    public class DotNetNoteMainSummaryTagHelper : TagHelper
    {
        private readonly INoteRepository _repository;

        /// <summary>
        /// 게시판 카테고리 : Notice, Free, Data, QnA, ... 
        /// </summary>

        public string Category { get; set; }
        public DotNetNoteMainSummaryTagHelper(INoteRepository repository)
        {
            _repository = repository;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            string s = "";
            var list = _repository.GetNoteSummaryByCategory(Category);

            foreach (var l in list)
            {
                s += $"<div class='post_item'><div class='post_item_text'>"
                    + $"<span class='post_date'>"
                    + l.PostDate.ToString("yyyy-MM-dd")
                    + "</span><span class='post_title'>"
                    + "<a href='/DotNetNote/Details/" + l.Id + "'>"
                    + Dul.StringLibrary.CutStringUnicode(l.Title, 33)
                    + "</a></span></div></div>";
            }
            output.Content.AppendHtml(s);
        }
    }
}
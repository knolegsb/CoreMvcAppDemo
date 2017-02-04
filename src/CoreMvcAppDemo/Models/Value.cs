using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public class Value
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Text 속성은 필수입력값입니다.")]
        public string Text { get; set; }
    }
}

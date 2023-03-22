using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll_test.Models
{
    public class Salary
    {


        [Required(ErrorMessage = "Mời bạn nhập mức lương đóng bảo hiểm: ")]
        [Range(0, 999999999, ErrorMessage = "Số tiền phải lớn hơn 0")]
        public decimal premiumSalary { get; set; }

        [Required(ErrorMessage = "Mời bạn chọn vùng: ")]
        public decimal area { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập Lương: ")]
        [Range(0, 999999999, ErrorMessage = "Số tiền phải lớn hơn 0")]
        //[GreaterThan("premiumSalary", ErrorMessage ="Số tiền phải lớn hơn số tiền tối thiểu vùng")]
        public decimal salary { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập số người phụ thuộc: ")]
        [Range(0, 999, ErrorMessage = "Số người phụ thuộc ít nhất là 0")]
        public int numberOfDependents { get; set; }

        // 10.5% bảo hiểm 
        public decimal? premium { get; set; }

        // thu nhập trước thuế (sau khi trừ đi 10.5% bảo hiểm)
        public decimal incomeBeforeTax {get;set;}

        //thu nhập chịu thuế
        public decimal incomeTaxes { get; set; }
        //thuế thu nhập cá nhân
        public decimal personalIncomeTax { get; set; }

        public decimal finalSalary { get; set; }



    }
}
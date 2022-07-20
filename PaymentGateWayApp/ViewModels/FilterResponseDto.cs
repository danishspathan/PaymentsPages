using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.ViewModels
{
    public class FilterResponseDto
    {
        public int headerId { get; set; }
        public string Title { get; set; }
        public string CompanyUrl { get; set; }
        public string CompanyRevenue { get; set; }
        public List<StateDto> stateList { get; set; }
        public List<CountryDto> countryList { get; set; }
        public List<SubCategoryDto> subCategoryList { get; set; }
        public List<IndustryDto> industryList { get; set; }
        public List<IndustryNameDto> industryNameList { get; set; }
        public List<PositionDto> positionList { get; set; }
        public int status { get; set; }

    }
    public class StateDto
    {
        public int StateId { get; set; }
        public int recordStatus { get; set; }
    }
    public class CountryDto
    {
        public int CountryId { get; set; }
        public int recordStatus { get; set; }
    }
    public class SubCategoryDto
    {
        public int SubCategoryId { get; set; }
        public int recordStatus { get; set; }
    }
    public class IndustryDto
    {
        public int IndustryId { get; set; }
        public int recordStatus { get; set; }
    }
    public class IndustryNameDto
    {
        public int IndustryId { get; set; }
        public int recordStatus { get; set; }
    }
    public class PositionDto
    {
        public string position { get; set; }
        public int recordStatus { get; set; }
    }

}
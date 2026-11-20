namespace NippoControlSystem.ApplicationService.Dtos
{
    public class InspectionDataDto
    {
        public List<InspectionRecordDto> Records { get; set; } = new();
    }
    public class InspectionRecordDto
    {
        public string InspectionDate { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string ResultValue { get; set; } = string.Empty;
    }
}

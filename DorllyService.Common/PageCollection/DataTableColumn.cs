namespace DorllyService.Common
{
    public class DataTableColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchStruct search { get; set; }
    }
}

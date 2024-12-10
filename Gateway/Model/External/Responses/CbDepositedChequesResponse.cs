namespace Gateway.Model.External.Responses
{
    public class CbDepositedChequesResponse
    {
        public long? Id { get; set; }
        public long? AccountNo { get; set; }
        public string InstrumentDate { get; set; }
        public long? InstrumentNo { get; set; }
        public string InstrumentBank { get; set; }
        public string InstrumentBranch { get; set; }
        public long? InstrumentAmount { get; set; }
        public string OwcLngStatus { get; set; }
        public string DepositedDate { get; set; }
        public string DrawerName { get; set; }
    }
}

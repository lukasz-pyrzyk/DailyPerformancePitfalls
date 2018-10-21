namespace DPF.WebApi.BankApi
{
    /// <summary>
    /// Represents result of the bond placement
    /// </summary>

    public class Bond
    {
        public string Auctiondate { get; set; }
        public int Auctionnum { get; set; }
        public string Valcode { get; set; }
        public string Stockcode { get; set; }
        public string Paydate { get; set; }
        public string Repaydate { get; set; }
        public int Stockrestrict { get; set; }
        public int Stockrestrictn { get; set; }
        public double Incomelevel { get; set; }
        public double Avglevel { get; set; }
        public int Amount { get; set; }
        public int Amountn { get; set; }
        public double Attraction { get; set; }
    }
}

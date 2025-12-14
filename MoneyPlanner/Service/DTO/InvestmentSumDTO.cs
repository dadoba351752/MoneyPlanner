namespace MoneyPlanner.Service.DTO
{
    public class InvestmentSumDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal AverageBuyPrice { get; set; }
        public decimal TodayPrice { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
    }
}

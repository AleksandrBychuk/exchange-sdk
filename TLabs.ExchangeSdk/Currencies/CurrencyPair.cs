namespace TLabs.ExchangeSdk.Currencies
{
    public class CurrencyPair
    {
        public string Code => $"{CurrencyToId}_{CurrencyFromId}";

        /// <summary>
        /// Is currency pair halted
        /// </summary>
        public bool IsHalted { get; set; }

        /// <summary>
        /// Is currency pair halted
        /// </summary>
        public bool IsShowedForUsers { get; set; }

        /// <summary>
        /// Quote Currency Code
        /// </summary>
        public string CurrencyFromId { get; set; }

        public virtual Currency CurrencyFrom { get; set; }

        /// <summary>
        /// Base Currency Code
        /// </summary>
        public string CurrencyToId { get; set; }

        public virtual Currency CurrencyTo { get; set; }

        public string OverridedName { get; set; }

        /// <summary>
        /// How many decimal digits should price have after rounding
        /// </summary>
        public int DigitsPrice { get; set; }

        /// <summary>
        /// How many decimal digits should Order/Deal amount have after rounding
        /// </summary>
        public int DigitsAmount { get; set; }

        public bool SendToCoinmarketcap { get; set; } = true;

        public CurrencyPair()
        {
        }

        public CurrencyPair(string currencyToId, string currencyFromId)
        {
            CurrencyToId = currencyToId;
            CurrencyFromId = currencyFromId;
        }

        public CurrencyPair(string code) : this(GetCurrencyToId(code), GetCurrencyFromId(code))
        {
        }

        public static string GetCurrencyToId(string code) => code.Split(new char[] { '_' })[0];

        public static string GetCurrencyFromId(string code) => code.Split(new char[] { '_' })[1];

        public string GetDisplayName => OverridedName ?? (CurrencyToId + "/" + CurrencyFromId);

        public override string ToString() => $"{nameof(CurrencyPair)}({Code} {OverridedName}, {CurrencyToId}-{CurrencyFromId}, " +
            $"digits:{DigitsPrice} {DigitsAmount}, {(IsHalted ? "halted" : "")} {(IsShowedForUsers ? "showed" : "")})";
    }
}

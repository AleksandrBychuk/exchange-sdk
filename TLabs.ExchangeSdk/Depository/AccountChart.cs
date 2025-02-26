using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TLabs.ExchangeSdk.Depository
{
    public class AccountChart
    {
        [Key]
        public string Code { get; set; }

        public string Value { get; set; }

        /// <summary>
        /// Key for localization
        /// </summary>
        public string ValueKey { get; set; }

        public static readonly AccountChart Users = new AccountChart("1", "Средства пользователей", nameof(Users));

        public static readonly AccountChart FundsDeposit = new AccountChart("38", "Пополнение фондов", nameof(FundsDeposit));
        public static readonly AccountChart FundsWithdrawal = new AccountChart("39", "Выводы из фондов", nameof(FundsWithdrawal));
        public static readonly AccountChart Funds = new AccountChart("3", "Фонды", nameof(Funds));
        public static readonly AccountChart NodeAgregationFunds = new AccountChart("31", "Фонды агрегации средств ноды", nameof(NodeAgregationFunds));
        public static readonly AccountChart WithdrawalNetworkCommissionFunds = new AccountChart("32", "Фонды служебных расходов на комиссию сети вывода", nameof(WithdrawalNetworkCommissionFunds));
        public static readonly AccountChart FundAffiliateBonusesForDistribution = new AccountChart("346", "Фонд Affiliate бонусов для распределения", nameof(FundAffiliateBonusesForDistribution));
        public static readonly AccountChart FundAffiliateProfits = new AccountChart("347", "Фонд Affiliate прибыли", nameof(FundAffiliateProfits));
        public static readonly AccountChart FundBountyBonusesForDistribution = new AccountChart("349", "Фонд BuySell бонусов для распределения", nameof(FundBountyBonusesForDistribution));
        public static readonly AccountChart FundPaymentsComission = new AccountChart("351", "Фонд комиссий платежей", nameof(FundPaymentsComission));
        public static readonly AccountChart Nullification = new AccountChart("359", "Обнуление баланса", nameof(Nullification));

        public static readonly AccountChart FundBot = new AccountChart("361", "Фонды бота", nameof(FundBot));
        public static readonly AccountChart FundBotProfits = new AccountChart("362", "Фонды прибыли бота", nameof(FundBotProfits));

        public static readonly AccountChart Deposit = new AccountChart("5", "Введеные средства", nameof(Deposit));
        public static readonly AccountChart DepositAdmin = new AccountChart("51", "Введеные средства админом", nameof(DepositAdmin));
        public static readonly AccountChart DepositStaking = new AccountChart("54", "Введеные средства стейкингом", nameof(DepositStaking));
        public static readonly AccountChart DepositAirdrop = new AccountChart("53", "Введеные средства аирдропом", nameof(DepositAirdrop));
        public static readonly AccountChart DepositReplacement = new AccountChart("58", "Введеные средства взамен удаленной валюты", nameof(DepositReplacement));
        public static readonly AccountChart DepositCorrection = new AccountChart("59", "Корректировочное пополнение баланса", nameof(DepositCorrection));

        public static readonly AccountChart BlockedForWithdrawn = new AccountChart("6", "Заморозка средств к выводу", nameof(BlockedForWithdrawn));
        public static readonly AccountChart Withdrawn = new AccountChart("4", "Выведенные средства", nameof(Withdrawn));

        public static readonly AccountChart BlockedForExchangeTransfer = new AccountChart("431", "Заморозка средств при ExchangeTransfer", nameof(BlockedForExchangeTransfer));

        public static readonly AccountChart BlockedForOrder = new AccountChart("7", "Заморозка средств на ордер", nameof(BlockedForOrder));
        public static readonly AccountChart BlockedForCancelOrder = new AccountChart("71", "Заморозка средств при отмене ордера", nameof(BlockedForCancelOrder));
        public static readonly AccountChart BlockedForDeal = new AccountChart("73", "Заморозка средств при сделке", nameof(BlockedForDeal));
        public static readonly AccountChart OnOrders = new AccountChart("75", "Средства на ордерах", "OnOrders");

        public static readonly AccountChart NetworkComission = new AccountChart("8", "Комиссия блокчейна", nameof(NetworkComission));
        public static readonly AccountChart ColdWallets = new AccountChart("11", "Холодные кошельки", nameof(ColdWallets));

        public static readonly AccountChart StakingBlockchainAccruals = new AccountChart("851", "Стейкинг Начисления в блокчейне", nameof(StakingBlockchainAccruals));
        public static readonly AccountChart StakingFundProfits = new AccountChart("852", "Стейкинг фонд прибыли", nameof(StakingFundProfits));
        public static readonly AccountChart StakingLocked = new AccountChart("855", "Стейкинг замороженные средства", nameof(StakingLocked));
        public static readonly AccountChart StakingLockedWithdrawal = new AccountChart("856", "Стейкинг замороженные средства на выводе", nameof(StakingLockedWithdrawal));

        public static readonly AccountChart CurrencyListingPaymentBlocked = new AccountChart("871", "CurrencyListing блокировка оплаты", nameof(CurrencyListingPaymentBlocked));

        public static readonly AccountChart CurrencyOfferingsBlocked = new AccountChart("901", "Этап блокировки на CurrencyOffering", nameof(CurrencyOfferingsBlocked));
        public static readonly AccountChart CurrencyOfferingsVesting = new AccountChart("903", "Вестинг на CurrencyOffering", nameof(CurrencyOfferingsVesting));

        /// <summary>Accounts that have UserId</summary>
        public static readonly List<AccountChart> UsersPersonalCharts = new List<AccountChart>
        {
            Users,
            BlockedForExchangeTransfer,
            BlockedForOrder, BlockedForCancelOrder, BlockedForDeal, OnOrders,
            BlockedForWithdrawn,
            StakingLocked, StakingLockedWithdrawal,
            CurrencyListingPaymentBlocked,
            CurrencyOfferingsBlocked, CurrencyOfferingsVesting,

            FundBot,
        };

        public static readonly List<AccountChart> All = new List<AccountChart>(UsersPersonalCharts)
        {
            Funds,
            FundAffiliateBonusesForDistribution, FundAffiliateProfits,
            FundBot, FundBotProfits, FundPaymentsComission, FundsDeposit, FundsWithdrawal,
            FundBountyBonusesForDistribution,
            Nullification,

            Deposit, DepositAdmin, DepositStaking, DepositAirdrop, DepositReplacement, DepositCorrection,

            Withdrawn,
            ColdWallets,
            NetworkComission, WithdrawalNetworkCommissionFunds,
            NodeAgregationFunds,

            StakingBlockchainAccruals, StakingFundProfits,
        };

        public static readonly List<AccountChart> DepositCharts = new List<AccountChart>
        {
            Deposit, DepositAdmin, DepositStaking, DepositAirdrop, DepositReplacement, DepositCorrection,
        };

        private AccountChart(string code, string value, string valueKey)
        {
            Code = code;
            Value = value;
            ValueKey = valueKey;
        }

        public AccountChart()
        {
        }
    }
}

using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TLabs.DotnetHelpers;
using TLabs.ExchangeSdk.CryptoAdapters.NownodesApi;
using TLabs.ExchangeSdk.Currencies;

namespace TLabs.ExchangeSdk.CryptoAdapters
{
    public class ClientCryptoAdapters
    {
        private readonly CurrenciesCache _currenciesCache;
        private readonly ClientCryptoNownodes _clientCryptoNownodes;

        public ClientCryptoAdapters(
            CurrenciesCache currenciesCache,
            ClientCryptoNownodes clientCryptoNownodes)
        {
            _currenciesCache = currenciesCache;
            _clientCryptoNownodes = clientCryptoNownodes;
        }

        [Obsolete("Use GetDepositAddress() with adapterCode")]
        public async Task<AddressModel> GetWalletAddress(string currencyCode, string userId,
            ClientType clientType = ClientType.User)
        {
            string adapterId = _currenciesCache.GetAdapterId(currencyCode);
            var result = await $"{adapterId}/address".InternalApi()
                .SetQueryParam(nameof(userId), userId)
                .SetQueryParam(nameof(clientType), clientType)
                .GetJsonAsync<AddressModel>();
            return result;
        }

        public async Task<AddressModel> GetDepositAddress(string adapterCode, string userId,
            ClientType clientType = ClientType.User)
        {
            var result = await $"{adapterCode}/address".InternalApi()
                .SetQueryParam(nameof(userId), userId)
                .SetQueryParam(nameof(clientType), clientType)
                .GetJsonAsync<AddressModel>();
            return result;
        }

        public async Task<AdapterInfo> GetAdapterInfo(string mainCurrencyCode, string nownodesApiKey = null)
        {
            string adapterId = _currenciesCache.GetAdapterId(mainCurrencyCode);
            var cancelToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
            var result = await $"{adapterId}/adapter-info".InternalApi()
                .GetJsonAsync<AdapterInfo>(cancelToken);

            if (nownodesApiKey.HasValue() && result != null
                && !ClientCryptoNownodes.UnsupportedCurrencies.Contains(mainCurrencyCode))
            {
                result.LastBlockPublicNode = (await _clientCryptoNownodes
                    .GetLastBlockNum(nownodesApiKey, mainCurrencyCode).GetQueryResult()).Data;
            }

            return result;
        }

        public async Task<AddressOwner> GetAddressOwner(string address, string adapterCode)
        {
            var result = await $"{adapterCode}/address/{address}/own".InternalApi().GetJsonAsync<AddressOwner>();
            return result;
        }

        public async Task<decimal> GetDepositMinAmount(string currencyCode)
        {
            string adapterId = _currenciesCache.GetAdapterId(currencyCode);
            string resultStr = await $"{adapterId}/refill-min-amount/{currencyCode}".InternalApi()
                .GetStringAsync();
            decimal result = Convert.ToDecimal(resultStr);
            return result;
        }

        #region ETH

        public async Task<QueryResult<string>> ResendTransaction(string adapterCode, string txHash, decimal? newGasPrice = null)
        {
            string url = $"{adapterCode}/transactions/resend/{txHash}" +
                $"?newGasPrice={newGasPrice?.ToString(CultureInfo.InvariantCulture)}";
            var result = await url.InternalApi().PostJsonAsync<string>(null).GetQueryResult();
            Console.WriteLine($"ResendTransaction txHash change: {txHash} -> {result.Data}  {result.ErrorsString}");
            return result;
        }

        public async Task<QueryResult<string>> CancelTransaction(string adapterCode, string txHash, decimal? newGasPrice = null)
        {
            var result = await $"{adapterCode}/transactions/cancel/{txHash}?newGasPrice={newGasPrice}".InternalApi()
                .PostJsonAsync<string>(null).GetQueryResult();
            Console.WriteLine($"CancelTransaction txHash change: {txHash} -> {result.Data}  {result.ErrorsString}");
            return result;
        }

        public async Task<List<ConsolidationSetting>> GetConsolidationSettings(string mainCurrencyCode)
        {
            string adapterId = _currenciesCache.GetAdapterId(mainCurrencyCode);
            var result = await $"{adapterId}/settings/consolidation".InternalApi()
                .GetJsonAsync<List<ConsolidationSetting>>();
            return result;
        }

        public async Task<List<ConsolidationSetting>> SaveConsolidationSettings(string mainCurrencyCode, List<ConsolidationSetting> model)
        {
            string adapterId = _currenciesCache.GetAdapterId(mainCurrencyCode);
            var result = await $"{adapterId}/settings/consolidation".InternalApi()
                .PostJsonAsync<List<ConsolidationSetting>>(model);
            return result;
        }

        #endregion ETH

        #region TRON

        public async Task<TronParamsDto> TronGetParams() =>
            await $"trx/parameters".InternalApi().GetJsonAsync<TronParamsDto>();

        public async Task<TronParamsDto> TronSaveParams(TronParamsDto dto) =>
            await $"trx/parameters".InternalApi().PostJsonAsync<TronParamsDto>(dto);

        #endregion TRON
    }
}

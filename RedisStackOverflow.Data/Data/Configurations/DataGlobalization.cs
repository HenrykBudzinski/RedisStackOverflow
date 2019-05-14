using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Data.Configurations
{
    public static class DataGlobalization
    {
        private static readonly string DefaultCustomCultureInfoName = "Henryk Budzinski";
        private static readonly string DefaultCustomCultureInfoKey = "hebu";
        private static readonly string DefaultCustomCultureInfoThreeLetter = "heb";
        private static readonly string DefaultCustomCultureInfoTwoLetter = "hb";

        static DataGlobalization()
        {
            try
            {
                CultureAndRegionInfoBuilder.Unregister(DefaultCustomCultureInfoKey);
            }
            catch(Exception ex)
            {
                // Log
            }

            try
            {
                var _cultureInfo =
                    new CultureAndRegionInfoBuilder(
                        DefaultCustomCultureInfoKey,
                        CultureAndRegionModifiers.None);
                _cultureInfo.LoadDataFromCultureInfo(CultureInfo.GetCultureInfo(1041));
                _cultureInfo.RegionEnglishName = DefaultCustomCultureInfoName;
                _cultureInfo.CultureEnglishName = DefaultCustomCultureInfoName;
                _cultureInfo.CultureNativeName = DefaultCustomCultureInfoName;
                _cultureInfo.RegionNativeName = DefaultCustomCultureInfoName;
                _cultureInfo.ThreeLetterISORegionName = DefaultCustomCultureInfoThreeLetter;
                _cultureInfo.ThreeLetterISOLanguageName = DefaultCustomCultureInfoThreeLetter;
                _cultureInfo.ThreeLetterWindowsLanguageName = DefaultCustomCultureInfoThreeLetter;
                _cultureInfo.ThreeLetterWindowsRegionName = DefaultCustomCultureInfoThreeLetter;
                _cultureInfo.TwoLetterISORegionName = DefaultCustomCultureInfoTwoLetter;
                _cultureInfo.TwoLetterISOLanguageName = DefaultCustomCultureInfoTwoLetter;
                _cultureInfo.GregorianDateTimeFormat.DateSeparator = "-";
                _cultureInfo.ISOCurrencySymbol = "BRL";
                _cultureInfo.CurrencyNativeName = "Real";
                _cultureInfo.CurrencyEnglishName = "Real";
                _cultureInfo.Register();
            }
            catch (Exception ex)
            {
                //  DEBUG
                //  Precisa abrir o VS em modo administrador.

                //  RELEASE
                //  O exe precisa de permissão para acessar os arquivos de globalização
                //  do sistema.
            }
        }

        public static CultureInfo GetDefaultCultureInfo()
        {
            CultureInfo cultureInfo = null;
            try
            {
                cultureInfo = CultureInfo.GetCultureInfo(DefaultCustomCultureInfoKey);
            }
            catch
            {
                cultureInfo = CultureInfo.CurrentUICulture ?? CultureInfo.CurrentCulture;
            }

            return cultureInfo;
        }
    }
}

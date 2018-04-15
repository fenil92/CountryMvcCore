using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVCAuth.Data;
using MVCAuth.Models;
using Newtonsoft.Json;

namespace MVCAuth
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any countries.
                if (context.Country.Any())
                {
                    return;   // DB has been seeded
                }
                string json = @"[
                    {countryName: 'Afghanistan', countryId: 'AF'}, 
                    {countryName: 'Åland Islands', countryId: 'AX'}, 
                    {countryName: 'Albania', countryId: 'AL'}, 
                    {countryName: 'Algeria', countryId: 'DZ'}, 
                    {countryName: 'American Samoa', countryId: 'AS'}, 
                    {countryName: 'AndorrA', countryId: 'AD'}, 
                    {countryName: 'Angola', countryId: 'AO'}, 
                    {countryName: 'Anguilla', countryId: 'AI'}, 
                    {countryName: 'Antarctica', countryId: 'AQ'}, 
                    {countryName: 'Antigua and Barbuda', countryId: 'AG'}, 
                    {countryName: 'Argentina', countryId: 'AR'}, 
                    {countryName: 'Armenia', countryId: 'AM'}, 
                    {countryName: 'Aruba', countryId: 'AW'}, 
                    {countryName: 'Australia', countryId: 'AU'}, 
                    {countryName: 'Austria', countryId: 'AT'}, 
                    {countryName: 'Azerbaijan', countryId: 'AZ'}, 
                    {countryName: 'Bahamas', countryId: 'BS'}, 
                    {countryName: 'Bahrain', countryId: 'BH'}, 
                    {countryName: 'Bangladesh', countryId: 'BD'}, 
                    {countryName: 'Barbados', countryId: 'BB'}, 
                    {countryName: 'Belarus', countryId: 'BY'}, 
                    {countryName: 'Belgium', countryId: 'BE'}, 
                    {countryName: 'Belize', countryId: 'BZ'}, 
                    {countryName: 'Benin', countryId: 'BJ'}, 
                    {countryName: 'Bermuda', countryId: 'BM'}, 
                    {countryName: 'Bhutan', countryId: 'BT'}, 
                    {countryName: 'Bolivia', countryId: 'BO'}, 
                    {countryName: 'Bosnia and Herzegovina', countryId: 'BA'}, 
                    {countryName: 'Botswana', countryId: 'BW'}, 
                    {countryName: 'Bouvet Island', countryId: 'BV'}, 
                    {countryName: 'Brazil', countryId: 'BR'}, 
                    {countryName: 'British Indian Ocean Territory', countryId: 'IO'}, 
                    {countryName: 'Brunei Darussalam', countryId: 'BN'}, 
                    {countryName: 'Bulgaria', countryId: 'BG'}, 
                    {countryName: 'Burkina Faso', countryId: 'BF'}, 
                    {countryName: 'Burundi', countryId: 'BI'}, 
                    {countryName: 'Cambodia', countryId: 'KH'}, 
                    {countryName: 'Cameroon', countryId: 'CM'}, 
                    {countryName: 'Canada', countryId: 'CA'}, 
                    {countryName: 'Cape Verde', countryId: 'CV'}, 
                    {countryName: 'Cayman Islands', countryId: 'KY'}, 
                    {countryName: 'Central African Republic', countryId: 'CF'}, 
                    {countryName: 'Chad', countryId: 'TD'}, 
                    {countryName: 'Chile', countryId: 'CL'}, 
                    {countryName: 'China', countryId: 'CN'}, 
                    {countryName: 'Christmas Island', countryId: 'CX'}, 
                    {countryName: 'Cocos (Keeling) Islands', countryId: 'CC'}, 
                    {countryName: 'Colombia', countryId: 'CO'}, 
                    {countryName: 'Comoros', countryId: 'KM'}, 
                    {countryName: 'Congo', countryId: 'CG'}, 
                    {countryName: 'Congo, The Democratic Republic of the', countryId: 'CD'}, 
                    {countryName: 'Cook Islands', countryId: 'CK'}, 
                    {countryName: 'Costa Rica', countryId: 'CR'}, 
                    {countryName: 'Cote D\'Ivoire', countryId: 'CI'}, 
                    {countryName: 'Croatia', countryId: 'HR'}, 
                    {countryName: 'Cuba', countryId: 'CU'}, 
                    {countryName: 'Cyprus', countryId: 'CY'}, 
                    {countryName: 'Czech Republic', countryId: 'CZ'}, 
                    {countryName: 'Denmark', countryId: 'DK'}, 
                    {countryName: 'Djibouti', countryId: 'DJ'}, 
                    {countryName: 'Dominica', countryId: 'DM'}, 
                    {countryName: 'Dominican Republic', countryId: 'DO'}, 
                    {countryName: 'Ecuador', countryId: 'EC'}, 
                    {countryName: 'Egypt', countryId: 'EG'}, 
                    {countryName: 'El Salvador', countryId: 'SV'}, 
                    {countryName: 'Equatorial Guinea', countryId: 'GQ'}, 
                    {countryName: 'Eritrea', countryId: 'ER'}, 
                    {countryName: 'Estonia', countryId: 'EE'}, 
                    {countryName: 'Ethiopia', countryId: 'ET'}, 
                    {countryName: 'Falkland Islands (Malvinas)', countryId: 'FK'}, 
                    {countryName: 'Faroe Islands', countryId: 'FO'}, 
                    {countryName: 'Fiji', countryId: 'FJ'}, 
                    {countryName: 'Finland', countryId: 'FI'}, 
                    {countryName: 'France', countryId: 'FR'}, 
                    {countryName: 'French Guiana', countryId: 'GF'}, 
                    {countryName: 'French Polynesia', countryId: 'PF'}, 
                    {countryName: 'French Southern Territories', countryId: 'TF'}, 
                    {countryName: 'Gabon', countryId: 'GA'}, 
                    {countryName: 'Gambia', countryId: 'GM'}, 
                    {countryName: 'Georgia', countryId: 'GE'}, 
                    {countryName: 'Germany', countryId: 'DE'}, 
                    {countryName: 'Ghana', countryId: 'GH'}, 
                    {countryName: 'Gibraltar', countryId: 'GI'}, 
                    {countryName: 'Greece', countryId: 'GR'}, 
                    {countryName: 'Greenland', countryId: 'GL'}, 
                    {countryName: 'Grenada', countryId: 'GD'}, 
                    {countryName: 'Guadeloupe', countryId: 'GP'}, 
                    {countryName: 'Guam', countryId: 'GU'}, 
                    {countryName: 'Guatemala', countryId: 'GT'}, 
                    {countryName: 'Guernsey', countryId: 'GG'}, 
                    {countryName: 'Guinea', countryId: 'GN'}, 
                    {countryName: 'Guinea-Bissau', countryId: 'GW'}, 
                    {countryName: 'Guyana', countryId: 'GY'}, 
                    {countryName: 'Haiti', countryId: 'HT'}, 
                    {countryName: 'Heard Island and Mcdonald Islands', countryId: 'HM'}, 
                    {countryName: 'Holy See (Vatican City State)', countryId: 'VA'}, 
                    {countryName: 'Honduras', countryId: 'HN'}, 
                    {countryName: 'Hong Kong', countryId: 'HK'}, 
                    {countryName: 'Hungary', countryId: 'HU'}, 
                    {countryName: 'Iceland', countryId: 'IS'}, 
                    {countryName: 'India', countryId: 'IN'}, 
                    {countryName: 'Indonesia', countryId: 'ID'}, 
                    {countryName: 'Iran, Islamic Republic Of', countryId: 'IR'}, 
                    {countryName: 'Iraq', countryId: 'IQ'}, 
                    {countryName: 'Ireland', countryId: 'IE'}, 
                    {countryName: 'Isle of Man', countryId: 'IM'}, 
                    {countryName: 'Israel', countryId: 'IL'}, 
                    {countryName: 'Italy', countryId: 'IT'}, 
                    {countryName: 'Jamaica', countryId: 'JM'}, 
                    {countryName: 'Japan', countryId: 'JP'}, 
                    {countryName: 'Jersey', countryId: 'JE'}, 
                    {countryName: 'Jordan', countryId: 'JO'}, 
                    {countryName: 'Kazakhstan', countryId: 'KZ'}, 
                    {countryName: 'Kenya', countryId: 'KE'}, 
                    {countryName: 'Kiribati', countryId: 'KI'}, 
                    {countryName: 'Korea, Democratic People\'S Republic of', countryId: 'KP'}, 
                    {countryName: 'Korea, Republic of', countryId: 'KR'}, 
                    {countryName: 'Kuwait', countryId: 'KW'}, 
                    {countryName: 'Kyrgyzstan', countryId: 'KG'}, 
                    {countryName: 'Lao People\'S Democratic Republic', countryId: 'LA'}, 
                    {countryName: 'Latvia', countryId: 'LV'}, 
                    {countryName: 'Lebanon', countryId: 'LB'}, 
                    {countryName: 'Lesotho', countryId: 'LS'}, 
                    {countryName: 'Liberia', countryId: 'LR'}, 
                    {countryName: 'Libyan Arab Jamahiriya', countryId: 'LY'}, 
                    {countryName: 'Liechtenstein', countryId: 'LI'}, 
                    {countryName: 'Lithuania', countryId: 'LT'}, 
                    {countryName: 'Luxembourg', countryId: 'LU'}, 
                    {countryName: 'Macao', countryId: 'MO'}, 
                    {countryName: 'Macedonia, The Former Yugoslav Republic of', countryId: 'MK'}, 
                    {countryName: 'Madagascar', countryId: 'MG'}, 
                    {countryName: 'Malawi', countryId: 'MW'}, 
                    {countryName: 'Malaysia', countryId: 'MY'}, 
                    {countryName: 'Maldives', countryId: 'MV'}, 
                    {countryName: 'Mali', countryId: 'ML'}, 
                    {countryName: 'Malta', countryId: 'MT'}, 
                    {countryName: 'Marshall Islands', countryId: 'MH'}, 
                    {countryName: 'Martinique', countryId: 'MQ'}, 
                    {countryName: 'Mauritania', countryId: 'MR'}, 
                    {countryName: 'Mauritius', countryId: 'MU'}, 
                    {countryName: 'Mayotte', countryId: 'YT'}, 
                    {countryName: 'Mexico', countryId: 'MX'}, 
                    {countryName: 'Micronesia, Federated States of', countryId: 'FM'}, 
                    {countryName: 'Moldova, Republic of', countryId: 'MD'}, 
                    {countryName: 'Monaco', countryId: 'MC'}, 
                    {countryName: 'Mongolia', countryId: 'MN'}, 
                    {countryName: 'Montserrat', countryId: 'MS'}, 
                    {countryName: 'Morocco', countryId: 'MA'}, 
                    {countryName: 'Mozambique', countryId: 'MZ'}, 
                    {countryName: 'Myanmar', countryId: 'MM'}, 
                    {countryName: 'Namibia', countryId: 'NA'}, 
                    {countryName: 'Nauru', countryId: 'NR'}, 
                    {countryName: 'Nepal', countryId: 'NP'}, 
                    {countryName: 'Netherlands', countryId: 'NL'}, 
                    {countryName: 'Netherlands Antilles', countryId: 'AN'}, 
                    {countryName: 'New Caledonia', countryId: 'NC'}, 
                    {countryName: 'New Zealand', countryId: 'NZ'}, 
                    {countryName: 'Nicaragua', countryId: 'NI'}, 
                    {countryName: 'Niger', countryId: 'NE'}, 
                    {countryName: 'Nigeria', countryId: 'NG'}, 
                    {countryName: 'Niue', countryId: 'NU'}, 
                    {countryName: 'Norfolk Island', countryId: 'NF'}, 
                    {countryName: 'Northern Mariana Islands', countryId: 'MP'}, 
                    {countryName: 'Norway', countryId: 'NO'}, 
                    {countryName: 'Oman', countryId: 'OM'}, 
                    {countryName: 'Pakistan', countryId: 'PK'}, 
                    {countryName: 'Palau', countryId: 'PW'}, 
                    {countryName: 'Palestinian Territory, Occupied', countryId: 'PS'}, 
                    {countryName: 'Panama', countryId: 'PA'}, 
                    {countryName: 'Papua New Guinea', countryId: 'PG'}, 
                    {countryName: 'Paraguay', countryId: 'PY'}, 
                    {countryName: 'Peru', countryId: 'PE'}, 
                    {countryName: 'Philippines', countryId: 'PH'}, 
                    {countryName: 'Pitcairn', countryId: 'PN'}, 
                    {countryName: 'Poland', countryId: 'PL'}, 
                    {countryName: 'Portugal', countryId: 'PT'}, 
                    {countryName: 'Puerto Rico', countryId: 'PR'}, 
                    {countryName: 'Qatar', countryId: 'QA'}, 
                    {countryName: 'Reunion', countryId: 'RE'}, 
                    {countryName: 'Romania', countryId: 'RO'}, 
                    {countryName: 'Russian Federation', countryId: 'RU'}, 
                    {countryName: 'RWANDA', countryId: 'RW'}, 
                    {countryName: 'Saint Helena', countryId: 'SH'}, 
                    {countryName: 'Saint Kitts and Nevis', countryId: 'KN'}, 
                    {countryName: 'Saint Lucia', countryId: 'LC'}, 
                    {countryName: 'Saint Pierre and Miquelon', countryId: 'PM'}, 
                    {countryName: 'Saint Vincent and the Grenadines', countryId: 'VC'}, 
                    {countryName: 'Samoa', countryId: 'WS'}, 
                    {countryName: 'San Marino', countryId: 'SM'}, 
                    {countryName: 'Sao Tome and Principe', countryId: 'ST'}, 
                    {countryName: 'Saudi Arabia', countryId: 'SA'}, 
                    {countryName: 'Senegal', countryId: 'SN'}, 
                    {countryName: 'Serbia and Montenegro', countryId: 'CS'}, 
                    {countryName: 'Seychelles', countryId: 'SC'}, 
                    {countryName: 'Sierra Leone', countryId: 'SL'}, 
                    {countryName: 'Singapore', countryId: 'SG'}, 
                    {countryName: 'Slovakia', countryId: 'SK'}, 
                    {countryName: 'Slovenia', countryId: 'SI'}, 
                    {countryName: 'Solomon Islands', countryId: 'SB'}, 
                    {countryName: 'Somalia', countryId: 'SO'}, 
                    {countryName: 'South Africa', countryId: 'ZA'}, 
                    {countryName: 'South Georgia and the South Sandwich Islands', countryId: 'GS'}, 
                    {countryName: 'Spain', countryId: 'ES'}, 
                    {countryName: 'Sri Lanka', countryId: 'LK'}, 
                    {countryName: 'Sudan', countryId: 'SD'}, 
                    {countryName: 'Suriname', countryId: 'SR'}, 
                    {countryName: 'Svalbard and Jan Mayen', countryId: 'SJ'}, 
                    {countryName: 'Swaziland', countryId: 'SZ'}, 
                    {countryName: 'Sweden', countryId: 'SE'}, 
                    {countryName: 'Switzerland', countryId: 'CH'}, 
                    {countryName: 'Syrian Arab Republic', countryId: 'SY'}, 
                    {countryName: 'Taiwan, Province of China', countryId: 'TW'}, 
                    {countryName: 'Tajikistan', countryId: 'TJ'}, 
                    {countryName: 'Tanzania, United Republic of', countryId: 'TZ'}, 
                    {countryName: 'Thailand', countryId: 'TH'}, 
                    {countryName: 'Timor-Leste', countryId: 'TL'}, 
                    {countryName: 'Togo', countryId: 'TG'}, 
                    {countryName: 'Tokelau', countryId: 'TK'}, 
                    {countryName: 'Tonga', countryId: 'TO'}, 
                    {countryName: 'Trinidad and Tobago', countryId: 'TT'}, 
                    {countryName: 'Tunisia', countryId: 'TN'}, 
                    {countryName: 'Turkey', countryId: 'TR'}, 
                    {countryName: 'Turkmenistan', countryId: 'TM'}, 
                    {countryName: 'Turks and Caicos Islands', countryId: 'TC'}, 
                    {countryName: 'Tuvalu', countryId: 'TV'}, 
                    {countryName: 'Uganda', countryId: 'UG'}, 
                    {countryName: 'Ukraine', countryId: 'UA'}, 
                    {countryName: 'United Arab Emirates', countryId: 'AE'}, 
                    {countryName: 'United Kingdom', countryId: 'GB'}, 
                    {countryName: 'United States', countryId: 'US'}, 
                    {countryName: 'United States Minor Outlying Islands', countryId: 'UM'}, 
                    {countryName: 'Uruguay', countryId: 'UY'}, 
                    {countryName: 'Uzbekistan', countryId: 'UZ'}, 
                    {countryName: 'Vanuatu', countryId: 'VU'}, 
                    {countryName: 'Venezuela', countryId: 'VE'}, 
                    {countryName: 'Viet Nam', countryId: 'VN'}, 
                    {countryName: 'Virgin Islands, British', countryId: 'VG'}, 
                    {countryName: 'Virgin Islands, U.S.', countryId: 'VI'}, 
                    {countryName: 'Wallis and Futuna', countryId: 'WF'}, 
                    {countryName: 'Western Sahara', countryId: 'EH'}, 
                    {countryName: 'Yemen', countryId: 'YE'}, 
                    {countryName: 'Zambia', countryId: 'ZM'}, 
                    {countryName: 'Zimbabwe', countryId: 'ZW'}]";
                var result = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);
                context.Country.AddRange(result);
                context.SaveChanges();
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Requires using MvcMovie.Models;
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using PuppeteerSharp;

namespace LvActionNetCore
{
    class BotDavid
    {
        private int viewportWidth;
        private int viewportHeight;
        public BotDavid()
        {
            viewportWidth = 942;
            viewportHeight = 838;
        }
        
        // connect to lvaction page method
        public async Task<dynamic> ConnectRoulette(string userName, string password, int table, int amountTransfer)
        {
            try
            {
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions() {Headless = false});
                var page = await browser.PagesAsync();

                await page[0].GoToAsync("https://lvaction.com");

                await page[0].SetViewportAsync(new ViewPortOptions() {Width = viewportWidth, Height = viewportHeight});
                string ws = browser.WebSocketEndpoint;

                await page[0].WaitForSelectorAsync(".logo_area_right #account");
                await page[0].ClickAsync(".logo_area_right #account");

                await page[0].TypeAsync(".logo_area_right #account", userName);

                await page[0].WaitForSelectorAsync(".logo_area_right #password");
                await page[0].ClickAsync(".logo_area_right #password");

                await page[0].TypeAsync(".logo_area_right #password", password);

                await page[0].WaitForSelectorAsync(".logo_area_right #login_submit");
                await page[0].ClickAsync(".logo_area_right #login_submit");

                await page[0].WaitForNavigationAsync();

                // click bar menu
                await page[0].WaitForSelectorAsync(
                    ".row > .col-4 > .burger-container > #burger > .btmBar"
                );
                await page[0].ClickAsync(".row > .col-4 > .burger-container > #burger > .btmBar");

                //live casino mobile
                await page[0].WaitForSelectorAsync(
                    "#ctl00_ctl00_MobileMenu #ctl00_ctl00_LiveCasinoMobile"
                );
                await Task.Delay(3000);
                await page[0].ClickAsync("#ctl00_ctl00_MobileMenu #ctl00_ctl00_LiveCasinoMobile");
                
                await Task.Delay(3000);

                page = await browser.PagesAsync();
                page[1].SetViewportAsync(new ViewPortOptions() {Width = viewportWidth+10, Height = viewportHeight+10});
                // click on pick button
                await page[1]
                    .WaitForSelectorAsync(".balance > .ng-scope > .manualtransfer > .amounts > .btn:nth-child(4)");
                await page[1].ClickAsync(".balance > .ng-scope > .manualtransfer > .amounts > .btn:nth-child(4)");

                // click on input box 
                await page[1]
                    .WaitForSelectorAsync(".balance > .ng-scope > .manualtransfer > .ng-pristine > .ng-pristine");
                await page[1].ClickAsync(".balance > .ng-scope > .manualtransfer > .ng-pristine > .ng-pristine");

                // enter amount
                await page[1].TypeAsync(
                    ".balance > .ng-scope > .manualtransfer > .ng-pristine > .ng-pristine",
                    amountTransfer.ToString()
                );
                // click on transfer button
                await page[1].WaitForSelectorAsync(".balance > .ng-scope > .manualtransfer > .ng-valid > .btn");
                await page[1].ClickAsync(".balance > .ng-scope > .manualtransfer > .ng-valid > .btn");




                // roulette tab
                await page[1].WaitForSelectorAsync(
                    "body > .ng-scope > .container > .ng-scope:nth-child(2) > .ng-binding"
                );
                await page[1].ClickAsync(
                    "body > .ng-scope > .container > .ng-scope:nth-child(2) > .ng-binding"
                );

                // click on routlet 1-100 $$ WE NEED TO CHANGE THIS
                await page[1].WaitForSelectorAsync(
                    ".col:nth-child(1) > .game > .limit > .ng-scope:nth-child(1) > .box"
                );
                await page[1].ClickAsync(
                $".col: nth - child(1) > .game > .limit > .ng - scope:nth - child({table.ToString()}) > .box"
            );

                await page[1].WaitForNavigationAsync();

                page = await browser.PagesAsync();
                return page[2];
            }
            catch
            {
                return false;
            }
        }


        public async void StoreDrawResult()
        {

            // + create  date field 
                var d = new DateTime();
                
                var data = null;

                if (drawResult.indexOf("RED") === 1)
                {
                    data = { Day: d.getDay(), dateTime: d, draw: "red" };
                }
                else if (drawResult.indexOf("BLACK") === 1)
                {
                    data = { Day: d.getDay(), dateTime: d, draw: "black" };
                }
                else
                {
                    data = { Day: d.getDay(), dateTime: d, draw: "green" };
                }

                list.push(data);
                const csv = new objectToCsv(list);
                // Save to file:
                await csv.toDisk('./' + fileName);
                console.log('saving ' + drawResult.toUpper() + 'to file');

        }
    }
}

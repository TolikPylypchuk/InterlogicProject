System.register(["rxjs/add/operator/map", "rxjs/add/operator/catch", "rxjs/add/observable/throw", "moment", "fullcalendar", "fullcalendar/dist/locale/uk", "@angular/platform-browser-dynamic", "./app.module"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var moment, platform_browser_dynamic_1, app_module_1;
    return {
        setters: [
            function (_1) {
            },
            function (_2) {
            },
            function (_3) {
            },
            function (moment_1) {
                moment = moment_1;
            },
            function (_4) {
            },
            function (_5) {
            },
            function (platform_browser_dynamic_1_1) {
                platform_browser_dynamic_1 = platform_browser_dynamic_1_1;
            },
            function (app_module_1_1) {
                app_module_1 = app_module_1_1;
            }
        ],
        execute: function () {
            moment.locale("uk");
            platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.default);
        }
    };
});
//# sourceMappingURL=main.js.map
﻿!(function () {
    var e = {
        111: function () {
            !(function () {
                const e = {
                    init() {
                        new ApexCharts(document.querySelector("#bsb-chart-8"), {
                            series: [
                                {
                                    name: "Customers",
                                    data: [544, 455, 357, 556, 561, 658, 544, 455, 357, 556, 561, 658],
                                },
                            ],
                            chart: {
                                type: "bar",
                                sparkline: { enabled: !0 },
                            },
                            colors: ["var(--bs-primary)"],
                            xaxis: {
                                categories: [
                                    "Jun", "Jul", "Aug", "Sep", "Oct", "Nov",
                                    "Jun", "Jul", "Aug", "Sep", "Oct", "Nov"
                                ],
                            },
                            tooltip: { theme: "dark" },
                            dataLabels: {
                                enabled: true, // Bật hiển thị số liệu
                                style: {
                                    colors: ["#000"], // Màu của số
                                },
                            },
                        }).render();
                    },
                };
                function t() {
                    e.init();
                }
                "loading" === document.readyState
                    ? document.addEventListener("DOMContentLoaded", t)
                    : t(),
                    window.addEventListener("load", function () { }, !1);
            })();
        },
    },
        t = {};
    function n(r) {
        var o = t[r];
        if (void 0 !== o) return o.exports;
        var a = (t[r] = { exports: {} });
        return e[r](a, a.exports, n), a.exports;
    }
    (n.n = function (e) {
        var t =
            e && e.__esModule
                ? function () {
                    return e.default;
                }
                : function () {
                    return e;
                };
        return n.d(t, { a: t }), t;
    }),
        (n.d = function (e, t) {
            for (var r in t)
                n.o(t, r) &&
                    !n.o(e, r) &&
                    Object.defineProperty(e, r, { enumerable: !0, get: t[r] });
        }),
        (n.o = function (e, t) {
            return Object.prototype.hasOwnProperty.call(e, t);
        }),
        (function () {
            "use strict";
            n(111);
        })();
})();

!(function () {
    const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";

    function getCookie(name) {
        // Split cookies by semicolon
        var cookies = document.cookie.split(';');

        // Loop through each cookie
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            // Trim leading and trailing spaces
            cookie = cookie.trim();
            // Check if this cookie is the one we're looking for
            if (cookie.startsWith(name + '=')) {
                // Return the cookie value
                return cookie.substring(name.length + 1);
            }
        }
        // Return null if cookie not found
        return null;
    }

    var series = [0, 0, 0];
    $.ajax({
        url: `${decodeURIComponent(API_BASE_URL)}/Dashboard/piechart`,
        type: "get",
        headers: {
            "Authorization": "Bearer " + getCookie("token"),
        },
        contentType: "application/json",
        success: function (result, status, xhr) {
            series = result;
            renderPieChart();
        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });

    function renderPieChart() {
        //series: [42, 4, 16, 12, 4, 4, 18],
        var e = {
            460: function () {
                !(function () {
                    const e = {
                        init() {
                            new ApexCharts(document.querySelector("#bsb-chart-2"), {
                                series: series,
                                labels: ["Student", "Lecturer", "Staff"],
                                legend: { position: "bottom" },
                                theme: { palette: "palette1" },
                                chart: { type: "donut" },
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
            var i = (t[r] = { exports: {} });
            return e[r](i, i.exports, n), i.exports;
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
                n(460);
            })();
    }
  
})();

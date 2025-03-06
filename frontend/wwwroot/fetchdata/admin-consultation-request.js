// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";

// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
    console.error("AJAX Error:", error);
    console.error("Status:", status);
    console.error("Response:", xhr.responseText);
    //if (JSON.parse(xhr.responseText) != undefined) alert(JSON.parse(xhr.responseText).title);
    alert(xhr.responseText);
}
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

function sendRequest(method, endpoint, data = {}, successCallback, errorCallback = handleAjaxError) {
    var token = getCookie('token');
    $.ajax({
        url: `${decodeURIComponent(API_BASE_URL)}${endpoint}`,
        method: method,
        contentType: "application/json",
        headers: {
            Authorization: `Bearer ${token}`
        },
        data: method === "GET" ? data : JSON.stringify(data),
        success: successCallback,
        error: errorCallback,
    });
}

// 3. Các hàm API cụ thể
const ApiService = {
    // Lấy danh sách dữ liệu (ví dụ: sản phẩm)
    getAllConsultationRequests: function (successCallback) {
        sendRequest("GET", `/consultationrequests`, {}, successCallback);
    },

    // Cập nhật dữ liệu
    updateStatus: function (conre, status, successCallback) {
        sendRequest("PUT", `/consultationrequests/changetatus/${conre}/${status}`, {}, successCallback);
    },
};

// 4. Sự kiện liên quan đến giao diện (UI)
let consultationRequestsGlobal = [];
$(document).ready(function () {
    render();
    $(document).on('click', '.resolved', function () {
        var id = $(this).data("conre");
        ApiService.updateStatus(id,'resolved',function (data) {
            render();
        });

    });

    $(document).on('click', '.unresolved', function () {
        var id = $(this).data("conre");
        ApiService.updateStatus(id, 'unresolved', function (data) {
            render();
        });

    });
});

function render() {
    ApiService.getAllConsultationRequests(function (data) {
        consultationRequestsGlobal = data;
        $("#consultationrequests").html("");
        $.each(data, function (index, value) {
            var switchbtn = value.isResolved == true ?
                `
                            <div class="form-check form-switch">
                              <input class="form-check-input unresolved" style="height:1.65em;" data-conre="${value.consultationRequestId}" type="checkbox" role="switch" id="flexSwitchCheckDefault" checked>
                            </div>
                        `: `
                            <div class="form-check form-switch">
                              <input class="form-check-input resolved" style="height:1.65em;" data-conre="${value.consultationRequestId}" type="checkbox" role="switch" id="flexSwitchCheckDefault">
                            </div>
                        `;
            $("#consultationrequests").append(`
                <tr>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${index + 1}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold w-25 text-wrap">${value.createdAtString}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.contactName}</span>        
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.email}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.phoneNumber}</span>
                    </td>
                    <td class="align-middle text-wrap">
                        <span class="text-xs font-weight-bold">${value.message}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${switchbtn}</span>
                    </td>
                    <td class="align-middle">
                        <span class="text-xs font-weight-bold">${value.resolvedBy ? value.resolvedBy?.firstName + " " + value.resolvedBy?.lastName : ""}</span>
                    </td>
                </tr>
            `);
        });
    });
}
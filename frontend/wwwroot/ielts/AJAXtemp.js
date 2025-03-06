
// 1. Khai báo các biến và cấu hình chung
const API_BASE_URL = "http://localhost:5000/api"; // URL gốc của API

// 2. Các hàm tiện ích chung
function handleAjaxError(xhr, status, error) {
    console.error("AJAX Error:", error);
    console.error("Status:", status);
    console.error("Response:", xhr.responseText);
    alert("Có lỗi xảy ra, vui lòng thử lại sau.");
}

function sendRequest(method, endpoint, data = {}, successCallback, errorCallback = handleAjaxError) {
    $.ajax({
        url: `${decodeURIComponent(API_BASE_URL)}${endpoint}`,
        method: method,
        contentType: "application/json",
        data: method === "GET" ? data : JSON.stringify(data),
        success: successCallback,
        error: errorCallback,
    });
}

// 3. Các hàm API cụ thể
const ApiService = {
    // Lấy danh sách dữ liệu (ví dụ: sản phẩm)
    getProducts: function (successCallback) {
        sendRequest("GET", "/auth/AccessTokenAnonymous", {}, successCallback);
    },

    // Thêm mới dữ liệu
    addProduct: function (productData, successCallback) {
        sendRequest("POST", "/products", productData, successCallback);
    },

    // Cập nhật dữ liệu
    updateProduct: function (productId, productData, successCallback) {
        sendRequest("PUT", `/products/${productId}`, productData, successCallback);
    },

    // Xóa dữ liệu
    deleteProduct: function (productId, successCallback) {
        sendRequest("DELETE", `/products/${productId}`, {}, successCallback);
    },
};

// 4. Sự kiện liên quan đến giao diện (UI)
$(document).ready(function () {
    // Ví dụ: Lấy danh sách sản phẩm và hiển thị lên bảng
    $("#loadProductsBtn").click(function () {
        ApiService.getProducts(function (data) {
            const productTable = $("#productTable tbody");
            productTable.empty(); console.log(data);
            data.forEach(function (product) {
                productTable.append(`
                    <tr>
                        <td>${product.id}</td>
                        <td>${product.name}</td>
                        <td>${product.price}</td>
                        <td>
                            <button class="edit-btn" data-id="${product.id}">Sửa</button>
                            <button class="delete-btn" data-id="${product.id}">Xóa</button>
                        </td>
                    </tr>
                `);
            });
        });
    });

    // Xử lý sự kiện xóa sản phẩm
    $(document).on("click", ".delete-btn", function () {
        const productId = $(this).data("id");
        if (confirm("Bạn có chắc muốn xóa sản phẩm này?")) {
            ApiService.deleteProduct(productId, function () {
                alert("Xóa sản phẩm thành công!");
                $("#loadProductsBtn").click(); // Tải lại danh sách
            });
        }
    });

    // Tương tự, bạn có thể thêm sự kiện để thêm/sửa sản phẩm
});

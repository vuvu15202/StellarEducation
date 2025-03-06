var currentPage = 1;
var totalPages = 1;

function CallAjax(urlLink) {
    var token = getCookie("token");
    $.ajax({
        url: urlLink,
        type: 'GET',
        headers: {
            "Authorization": "" + token
        },
        success: function (data) {
            $('#projectRecord tbody').empty();

            // Thêm dữ liệu mới từ API vào bảng
            $.each(data.listView, function (index, item) {
                var status = item.discontinued ? 1 : 0
                var newRow =
                    `<tr>
                        <td>
                            <div class="d-flex px-2 py-1">
                                <div>
                                    <img src="${item.image}" class="avatar avatar-sm me-3" alt="user4">
                                </div>
                                <div class="d-flex flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">
                                        <a href="${item.href}">
                                            ${item.title}
                                        </a>
                                    </h6>
                                    <p class="text-xs text-secondary mb-0">michael@creative-tim.com</p>
                                </div>
                            </div>
                        </td>
                        <td class="align-middle text-center text-sm">
                            <span class="badge badge-sm bg-gradient-success">${item.typeText}</span>
                        </td>
                        <td class="align-middle text-center">
                            <span class="text-secondary text-xs font-weight-bold">${item.targetAmount}</span>
                        </td>
                        <td class="align-middle text-center">
                            <span class="text-secondary text-xs font-weight-bold">${item.total}</span>
                        </td>
                        <td class="align-middle text-center">
                            <span class="text-secondary text-xs font-weight-bold">${item.startDateText}</span>
                        </td>
                        <td class="align-middle text-center">
                            <span class="text-secondary text-xs font-weight-bold">${item.endDateText}</span>
                        </td>
                        <td class="align-middle text-center text-sm">
                            <button class="btn btn-sm bg-gradient-secondary mt-3 changeStatus" style="width: 150px" data-id="${item.projectId}" status="${status}"">${item.status}</button>
                        </td>
                    </tr>`

                $('#projectRecord tbody').append(newRow);
            });
            currentPage = data.pageNumber;
            totalPages = data.pageCount;
            var nextPage = currentPage + 1;
            var previousPage = currentPage - 1;
            var keyword = $('#inputForm').val();
            if (keyword) {
                $('#previousBtn').attr('page', previousPage + "&keyword=" + keyword)
                $('#nextBtn').attr('page', nextPage + "&keyword=" + keyword)
            } else {
                $('#previousBtn').attr('page', previousPage)
                $('#nextBtn').attr('page', nextPage)
            }
            if (data.isFirstPage || data.pageCount == 0) {
                $('#previousBtn').addClass("disabled")
            } else {
                $('#previousBtn').removeClass("disabled")
            }
            if (data.isLastPage || data.pageCount == 0) {
                $('#nextBtn').addClass("disabled")
            } else {
                $('#nextBtn').removeClass("disabled")
            }
        },
        error: function () {
        }
    });
}

$(".page-link").on("click", function () {
    var page = $(this).attr('page');
    url = urlBase + `?page=${page}`;
    CallAjax(url);
})

$("#inputForm").on("change", function () {
    var value = $(this).val();
    url = urlBase + `?keyword=${value}`;
    CallAjax(url);
})

$('body').on('click', '.changeStatus', function () {
    var status = $(this).attr("status");
    var id = $(this).attr("data-id");

    var formData = {
        status: status,
        id: id
    };

    $.ajax({
        type: 'POST',
        url: urlUpdate,
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (data) {
            console.log(data);
            CallAjax(urlBase);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
});

$(document).ready(function () {
    CallAjax(urlBase)
});
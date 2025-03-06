const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";

//let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    //document.getElementById('projectsActive').className = 'active';
    //document.getElementById("ProjectIdMomo").value = projectId;
    //document.getElementById("ProjectIdVnPay").value = projectId;
    //console.log(document.getElementById("ProjectIdVnPay").value)
    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
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

async function getAllCatogories() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/Categories`);
}

// -------------------------------------------------------------------------------
let categoriesGlobal = [];

async function pushDataOnLoad() {


    getAllCatogories().then(async (categories) => {
        const tbody = document.getElementById('categoryTableBody');
        tbody.innerHTML = '';
        categoriesGlobal = categories;
        $.each(categories, function (index, value) {
            const row = document.createElement('tr');
            row.innerHTML = `
                                <td class="align-middle">
                                    <span class="text-xs font-weight-bold">${value.categoryId}</span>
                                </td>
                                <td class="align-middle">
                                    <image src="${value.image}" style="height: 50px; width: fit-content;">
                                </td>
                                <td class="align-middle">
                                    <span class="text-xs font-weight-bold">${value.name}</span>
                                </td>
                                <td class="align-middle">
                                    <a href="javascript:;" class="btn btn-primary btn-sm ms-auto edit-category" data-bs-toggle="modal" data-bs-target="#editCate" data-name="${value.name}" data-image="${value.image}" data-id = "${value.categoryId}">
                                        Edit
                                    </a>
                                    <a href="javascript:;" class="btn btn-danger delete-category" data-id="${value.categoryId}">
                                        delete
                                    </a>
                                </td>
                            `;
            tbody.appendChild(row);
        });


    })


    
}

$('#searchCate').on('input', function () {
    var searchText = $(this).val().toLowerCase();
    var filter = categoriesGlobal.filter(function (category) {
        return category.name.toLowerCase().includes(searchText) ;
    });
    // showStudentList(filteredStudents);
    $("#categoryTableBody").html("");
    $.each(filter, function (index, value) {
        $("#categoryTableBody").append(`
            <tr>
                <td class="align-middle">
                                    <span class="text-xs font-weight-bold">${value.categoryId}</span>
                                </td>
                                <td class="align-middle">
                                    <image src="${value.image}" style="height: 50px; width: fit-content;">
                                </td>
                                <td class="align-middle">
                                    <span class="text-xs font-weight-bold">${value.name}</span>
                                </td>
                                <td class="align-middle">
                                    <a href="javascript:;" class="btn btn-primary btn-sm ms-auto edit-category" data-bs-toggle="modal" data-bs-target="#editCate" data-name="${value.name}" data-image="${value.image}" data-id = "${value.categoryId}">
                                        Edit
                                    </a>
                                    <a href="javascript:;" class="btn btn-danger delete-category" data-id="${value.categoryId}">
                                        delete
                                    </a>
                                </td>
            </tr>
        `);
    });
});



$(document).ready(function () {



    $("#createCategory").click(function () {
        const name = $('#name').val();
        const image = $('#image').prop('files')[0];

        // Tạo một đối tượng để chứa các giá trị
        const formData = new FormData();
        formData.append('name', name);
        formData.append('imageFile', image);

        $.ajax({
            type: "post",
            url: `${decodeURIComponent(API_BASE_URL)}/Categories`,
            data: formData,
            contentType: false,
            processData: false,
            success: function (result, status, xhr) {
                if (xhr.status === 200) {

                    location.reload();
                }
            },
            error: function (xhr, status, error) {
                const errorMessage = $('#createError');
                errorMessage.text('');
                if (xhr.status === 404 && xhr.responseText === "Only .jpg,.png,.jpeg extensions are allowed") {
                    errorMessage.text(xhr.responseText);
                } else {
                    errorMessage.text('Bạn cần upload file dưới định dạng .jpg, .png, jpeg!');
                }
            }
        });
    });

    $(document).on('click', '.edit-category', function () {
        // Get the data from the button's data attributes
        var id = $(this).data('id');
        var name = $(this).data('name');
        var imageUrl = $(this).data('image');

        // Populate the modal fields
        $('#editUserId').val(id);
        $('#editName').val(name);
        $('#imagePreview').remove();

        // For the image, we can't set the value of a file input,
        // but we can show a preview of the current image
        var imagePreview = $('<img>').attr('src', imageUrl).attr('id', 'imagePreview').css('max-width', '100%');
        $('#editImage').after(imagePreview);

        $('#editImage').val('');

        $('#editImage').change(function (event) {
            var file = event.target.files[0];
            if (file) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    // Remove existing preview if any
                    imagePreview.remove();

                    // Create and add new image preview
                    var newPreview = $('<img>')
                        .attr('src', e.target.result)
                        .attr('id', 'imagePreview')
                        .css('max-width', '100%')
                        .css('margin-top', '10px');
                    $('#editImage').after(newPreview);
                }
                reader.readAsDataURL(file);
            } else {
                // If no file is selected, remove the preview
                $('#imagePreview').remove();
            }
        });
    });


    $('#saveEditCateBtn').click(function () {
        var id = $('#editUserId').val();
        var name = $('#editName').val();
        var imageFile = $('#editImage')[0].files[0];

        const formData = new FormData();
        formData.append('categoryId', id);
        formData.append('name', name);
        formData.append('imageFile', imageFile);

        $.ajax({
            type: "post",
            url: `${decodeURIComponent(API_BASE_URL)}/Categories/Update`,
            data: formData,
            contentType: false,
            processData: false,
            success: function (result, status, xhr) {
                if (xhr.status === 200) {

                    location.reload();
                }
            },
            error: function (xhr, status, error) {
                const errorMessage = $('#editError');
                errorMessage.text('');
                if (xhr.status === 500 && xhr.responseText === "Only .jpg,.png,.jpeg extensions are allowed") {
                    errorMessage.text(xhr.responseText);
                } else {
                    errorMessage.text('An error occurred while editing the category.');
                }
            }
        });

    })

    $(document).on('click', '.delete-category', function () {
        var id = $(this).data('id');
        if (confirm('Are you sure you want to delete this category ?')) {
            console.log(id);
            $.ajax({
                url: `${decodeURIComponent(API_BASE_URL)}/Categories/${id}`,
                type: 'DELETE',
                contentType: 'application/json',
                success: function (result, status, xhr) {
                    console.log(xhr.status);
                    if (xhr.status === 200) {

                        alert("Category deleted successfully");
                        // Optionally, update the UI or redirect
                        location.reload();
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                    alert("An error occurred while deleting the category");
                }
            });
        }

    });

    
});



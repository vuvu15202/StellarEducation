const url = 'http://localhost:5000/api';

let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

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
async function getAllNoti() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/Notifications`);
}

async function getAllUsers() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/admin/GetAllUsers`);
}

// -------------------------------------------------------------------------------
let courseenrollGlobal = {};
async function pushDataOnLoad() {
    

    getAllNoti().then(async (notis) => {
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        $.each(notis, function (index, value) {
            $("#Noties").append(`<tr>
                        <td style="max-width: 150px;">
                            <div class="d-flex px-2 py-1">
                                <div>
                                    <img src="../assets/img/team-2.jpg" class="avatar avatar-sm me-3" alt="user1">
                                </div>
                                <div style="width: 190px !important;" class="d-flex flex-column justify-content-center  text-wrap">
                                    <h6 class="mb-0 text-sm">${value.notificationTo.name}</h6>
                                    <p class="text-xs text-secondary mb-0">${value.notificationTo.email}</p>
                                </div>
                            </div>
                        </td>
                        <td class="align-middle text-center text-wrap" >
                            <div  style="width: 100px !important;" class="text-secondary text-xs font-weight-bold">${value.notificationAt}</div>
                        </td>
                        <td class="align-middle text-center text-wrap">
                            <span class="text-secondary text-xs font-weight-bold">${value.notificationTitle}</span>
                        </td>
                        <td class="align-middle text-center text-sm text-wrap"  >
                            <div style="width: 190px !important;" class="text-secondary text-xs font-weight-bold text-wrap">${value.notificationContent}</div>
                        </td>
                        <td class="align-middle">
                            <a href="javascript:;" class="text-secondary font-weight-bold text-xs" data-toggle="tooltip" data-original-title="Edit user">
                                <span class="badge badge-sm bg-gradient-success">Edit</span>
                            </a>
                        </td>
                    </tr>
        `);
        });
        
        
    })
    

    getAllUsers().then(users => {
        let html = users.map(user => {
            return `<option value="${user.userId}">${user.userId}: ${user.firstName} ${user.firstName}</option>`;
        });
        $("#selectUser").append(html.join(''));
    })
}


$(document).ready(function () {
    $("#sendNoti").click(function () {
        var selectUser = $("#selectUser").val();
        var title = $("#title").val();
        var content = $("#content").val();

        var formData = {
            notificationTo: selectUser,
            notificationTitle: title,
            notificationContent: content
        };

        $.ajax({
            type: "POST",
            url: `${decodeURIComponent(API_BASE_URL)}/Notifications`, 
            data: JSON.stringify(formData),
            contentType: "application/json",
            success: function (result, status, xhr) {

                start();
            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }
        });
    });
});


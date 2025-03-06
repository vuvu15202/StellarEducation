const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";
var token = getCookie("token");

document.addEventListener('DOMContentLoaded', (event) => {

    fetch(`${decodeURIComponent(API_BASE_URL) }/Account/GetUserProfile`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        document.getElementById('phonenumber').value = data.phonenumber;
        document.getElementById('email').value = data.email;
        document.getElementById('firstname').value = data.firstName;
        document.getElementById('lastname').value = data.lastName;
        document.getElementById('address').value = data.address;
        document.getElementById('description').value = data.description;

        if (data.image) $("#userImage").attr('src', data.image);
        document.getElementById('userNameHeader').textContent = data.lastName + " " + data.firstName;
        document.getElementById('roleNameHeader').textContent = "";

        $('#editFirstName').val(data.firstName);
        $('#editLastName').val(data.lastName);
        $('#editEmail').val(data.email);
        $('#editPhoneNumber').val(data.phonenumber);
        $('#editAddress').val(data.address);
        $('#editDescription').val(data.description);
    })
    .catch(error => console.error('Error fetching user profile:', error));
});

document.getElementById('saveChangesBtn').addEventListener('click', () => {
    const firstName = document.getElementById('editFirstName').value;
    const lastName = document.getElementById('editLastName').value;
    const email = document.getElementById('editEmail').value;
    const phoneNumber = document.getElementById('editPhoneNumber').value;
    const address = document.getElementById('editAddress').value;
    const description = document.getElementById('editDescription').value;
    var image = $('#editImage')[0].files[0];

    // Tạo FormData thay vì object JSON
    const formData = new FormData();
    formData.append('firstName', firstName);
    formData.append('lastName', lastName);
    formData.append('email', email);
    formData.append('phoneNumber', phoneNumber);
    formData.append('address', address);
    formData.append('description', description);
    formData.append('image', image);

    fetch(`${decodeURIComponent(API_BASE_URL)}/Account/UpdateProfile`, {
        method: 'PUT',
        headers: {
            'Authorization': 'bearer ' + token
        },
        body: formData,
    }).then(response => {
        if (!response.ok) {
            throw new Error('Failed to update profile');
        }
        return response.json();
    }).then(data => {
        alert(data.message);
        window.location.href = '/Account/UserProfile';
    }).catch(error => {
        console.error('Error changing password:', error.message);
        alert(error.message);
    });
});
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

document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('changePasswordBtn').addEventListener('click', () => {
        const oldPassword = document.getElementById('oldPassword').value;
        const newPassword = document.getElementById('newPassword').value;
        const confirmPassword = document.getElementById('confirmPassword').value;

        const data = {
            oldPassword: oldPassword,
            newPassword: newPassword,
            confirmPassword: confirmPassword
        };
        fetch(`${decodeURIComponent(API_BASE_URL)}/Account/ChangePassword`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + token
            },
            body: JSON.stringify(data),
        }).then(response => {
            if (!response.ok) {
                return response.json().then(errorData => {
                    throw new Error(errorData.message || 'Failed to change password');
                });
            }
            return response.json();
        }).then(data => {
            alert(data.message);
            window.location.href = '/Account/UserProfile';
        }).catch(error => {
            console.error('Error changing password:', error.message);
            alert(error.message);
        });
    });
});
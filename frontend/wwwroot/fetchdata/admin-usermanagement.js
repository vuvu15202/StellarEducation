document.addEventListener('DOMContentLoaded', () => {
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
    let usersData = [];
    let rolesData = [];
    let sortOrder = {
        firstName: true,
        lastName: true,
        active: true,
        role: true
    };

    // Fetch users and store data
    fetch(`${decodeURIComponent(API_BASE_URL)}/Admin/GetAllUsers`)
        .then(response => response.json())
        .then(data => {
            usersData = data;  // Store the fetched data
            renderUsers(usersData);
        })
        .catch(error => console.error('Error fetching users:', error));

    // Fetch roles and store data
    fetch(`${decodeURIComponent(API_BASE_URL)}/Admin/GetAllRole`)
        .then(response => response.json())
        .then(data => {
            rolesData = data;
        })
        .catch(error => console.error('Error fetching roles:', error));

    // Render users function
    function renderUsers(users) {
        const tbody = document.getElementById('userTableBody');
        tbody.innerHTML = ''; // Clear existing rows
        users.forEach((user, index) => {
            const row = document.createElement('tr');

            row.innerHTML = `
                                <td>
                                    <span class="text-xs font-weight-bold">${index + 1}</span>
                                </td>
                                <td>
                                    <div class="d-flex px-2 py-1">
                                        <div class="d-flex flex-column justify-content-center">
                                            <p class="text-xs font-weight-bold mb-0">${user.firstName}</p>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <p class="text-xs font-weight-bold mb-0">${user.lastName}</p>
                                </td>
                                <td class="align-middle text-center text-sm">
                                    <span class="text-secondary text-xs font-weight-bold">${user.email}</span>
                                </td>
                                <td class="align-middle text-center">
                                    <span class="text-secondary text-xs font-weight-bold">${user.phone}</span>
                                </td>
                                <td class="align-middle text-center">
                                    <span class="text-secondary text-xs font-weight-bold">${user.address}</span>
                                </td>
                                <td class="align-middle text-center text-sm">
                                    <span class="badge badge-sm bg-gradient-${user.active ? 'success' : 'secondary'}">${user.active ? 'True' : 'False'}</span>
                                </td>
                                <td class="align-middle text-center text-sm">
                                    <span class="text-secondary text-xs font-weight-bold">${user.role.roleName}</span>
                                </td>
                                <td class="align-middle">
                                    <a href="javascript:;" class="text-secondary font-weight-bold text-xs edit-user" data-id="${user.userId}" data-firstname="${user.firstName}" data-lastname="${user.lastName}" data-email="${user.email}" data-address="${user.address}" data-phone="${user.phone}" data-active="${user.active}" data-roleid="${user.role.roleId}" data-rolename="${user.role.roleName}">
                                        Edit
                                    </a>
                                </td>
                            `;

            tbody.appendChild(row);
        });

        // Add event listeners for edit buttons
        document.querySelectorAll('.edit-user').forEach(button => {
            button.addEventListener('click', event => {
                const userId = event.target.getAttribute('data-id');
                const firstName = event.target.getAttribute('data-firstname');
                const lastName = event.target.getAttribute('data-lastname');
                const email = event.target.getAttribute('data-email');
                const phone = event.target.getAttribute('data-phone');
                const address = event.target.getAttribute('data-address');
                const active = event.target.getAttribute('data-active');
                const roleId = event.target.getAttribute('data-roleid');
                const roleName = event.target.getAttribute('data-rolename');

                document.getElementById('editUserId').value = userId;
                document.getElementById('editFirstName').value = firstName;
                document.getElementById('editLastName').value = lastName;
                document.getElementById('editEmail').value = email;
                document.getElementById('editPhone').value = phone;
                document.getElementById('editAddress').value = address;
                document.getElementById('editActive').value = active;

                console.log(document.getElementById('editActive').value)

                // Populate the roles dropdown
                const roleSelect = document.getElementById('editRole');
                roleSelect.innerHTML = ''; // Clear existing options
                rolesData.forEach(roleItem => {
                    const option = document.createElement('option');
                    option.value = roleItem.roleId;
                    option.text = roleItem.roleName;
                    roleSelect.appendChild(option);
                });
                roleSelect.value = roleId; // Set the selected role by roleId

                const editUserModal = new bootstrap.Modal(document.getElementById('editUserModal'));
                editUserModal.show();
            });
        });
    }

    // Add sorting event listeners to the table headers
    document.querySelectorAll('.sortable').forEach(header => {
        header.addEventListener('click', () => {
            const sortField = header.getAttribute('data-sort');
            sortOrder[sortField] = !sortOrder[sortField];  // Toggle sort order
            const sortedUsers = [...usersData].sort((a, b) => {
                if (sortField === 'active') {
                    return sortOrder[sortField] ? a[sortField] - b[sortField] : b[sortField] - a[sortField];
                } else if (sortField === 'role') {
                    return sortOrder[sortField] ? a.role.roleName.localeCompare(b.role.roleName) : b.role.roleName.localeCompare(a.role.roleName);
                } else {
                    return sortOrder[sortField] ? a[sortField].localeCompare(b[sortField]) : b[sortField].localeCompare(a[sortField]);
                }
            });
            renderUsers(sortedUsers);
        });
    });

    // Save edited user
    document.getElementById('saveEditUserBtn').addEventListener('click', () => {
        const userId = document.getElementById('editUserId').value;
        const firstName = document.getElementById('editFirstName').value;
        const lastName = document.getElementById('editLastName').value;
        const email = document.getElementById('editEmail').value;
        const phone = document.getElementById('editPhone').value;
        const address = document.getElementById('editAddress').value;
        const active = document.getElementById('editActive').value;
        const roleId = document.getElementById('editRole').value;

        const updatedUser = {
            userId: userId,
            firstName: firstName,
            lastName: lastName,
            email: email,
            phone: phone,
            address: address,
            active: active === 'true',
            roleId: roleId
        };

        fetch(`${decodeURIComponent(API_BASE_URL)}/Admin/UpdateUser`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedUser)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                const row = document.querySelector(`.edit-user[data-id="${userId}"]`).closest('tr');
                row.querySelector('td:nth-child(2) .font-weight-bold').innerText = data.firstName;
                row.querySelector('td:nth-child(3) .font-weight-bold').innerText = data.lastName;
                row.querySelector('td:nth-child(4) .text-secondary').innerText = data.email;
                row.querySelector('td:nth-child(5) .text-secondary').innerText = data.phone;
                row.querySelector('td:nth-child(6) .text-secondary').innerText = data.phone;
                row.querySelector('td:nth-child(7) .badge').innerText = data.active ? 'True' : 'False';
                row.querySelector('td:nth-child(7) .badge').className = `badge badge-sm bg-gradient-${data.active ? 'success' : 'secondary'}`;

                const editUserModal = bootstrap.Modal.getInstance(document.getElementById('editUserModal'));
                alert("Update Succesful");
                window.location.href = '/Admin/UserManagement';
            })
            .catch(error => {
                console.error('Error changing password:', error.message);
                alert(error.message);
            });
    });

    // Search by name, email, address, phone
    document.getElementById('searchInput').addEventListener('input', (event) => {
        const searchInput = document.getElementById('searchInput').value.toLowerCase();
        const filteredUsers = usersData.filter(user =>
            user.firstName.toLowerCase().includes(searchInput) ||
            user.lastName.toLowerCase().includes(searchInput) ||
            user.email.toLowerCase().includes(searchInput) ||
            user.phone.toLowerCase().includes(searchInput) ||
            user.address.toLowerCase().includes(searchInput)
        );
        renderUsers(filteredUsers);
    });
    // document.getElementById('searchInput').addEventListener('keyup', (event) => {
    //     if (event.key === 'Enter') {
    //         const searchInput = document.getElementById('searchInput').value.toLowerCase();
    //         const filteredUsers = usersData.filter(user =>
    //             user.firstName.toLowerCase().includes(searchInput) ||
    //             user.lastName.toLowerCase().includes(searchInput) ||
    //             user.email.toLowerCase().includes(searchInput) ||
    //             user.phone.toLowerCase().includes(searchInput) ||
    //             user.address.toLowerCase().includes(searchInput)
    //         );
    //         renderUsers(filteredUsers);
    //     }
    // });

    // Add User Modal functionality
    document.getElementById('saveAddUserBtn').addEventListener('click', () => {
        const userName = document.getElementById('addUserName').value;
        const firstName = document.getElementById('addFirstName').value;
        const lastName = document.getElementById('addLastName').value;
        const email = document.getElementById('addEmail').value;
        const phone = document.getElementById('addPhone').value;
        const address = document.getElementById('addAddress').value;
        const active = document.getElementById('addActive').value;
        const roleId = document.getElementById('addRole').value;
        const password = document.getElementById('addPassword').value;

        const newUser = {
            userName: userName,
            firstName: firstName,
            lastName: lastName,
            email: email,
            phone: phone,
            address: address,
            active: active === "true",
            roleId: parseInt(roleId, 10),
            password: password
        };

        fetch(`${decodeURIComponent(API_BASE_URL)}/Admin/CreateUser`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text(); // Response is just a status code
            })
            .then(() => {
                // Close the modal
                const addUserModal = bootstrap.Modal.getInstance(document.getElementById('addUserModal'));
                addUserModal.hide();

                // Clear form fields
                document.getElementById('addUserForm').reset();

                alert('User added successfully');
                window.location.href = '/Admin/UserManagement';
            })
            .catch(error => {
                console.error('Error creating user:', error.message);
                alert('Error creating user: ' + error.message);
            });
    });

    // Function to open the Add User Modal
    document.getElementById('openAddUserModalBtn').addEventListener('click', () => {
        // Populate the roles dropdown
        const roleSelect = document.getElementById('addRole');
        roleSelect.innerHTML = ''; // Clear existing options
        rolesData.forEach(roleItem => {
            const option = document.createElement('option');
            option.value = roleItem.roleId;
            option.text = roleItem.roleName;
            roleSelect.appendChild(option);
        });

        // Open the Add User Modal
        const addUserModal = new bootstrap.Modal(document.getElementById('addUserModal'));
        addUserModal.show();
    });

});
const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";



// Init When Load page
const initPage = async () => {
	await pushDataOnLoad();
	
    
}

async function getCourses() {
	const callApi = async (API_BASE_URL) => {
		return (await fetch(API_BASE_URL)).json();
	}
	return await callApi(`${decodeURIComponent(API_BASE_URL)}/courses`);
}

document.addEventListener("DOMContentLoaded", initPage);

async function pushDataOnLoad() {

	getCourses().then(async (courses) => {
		courses = courses.slice(0, 6);
		const wishlists = document.getElementById("wishlists");
		//render wishlist
		let wishlisthtml = await Promise.all(courses.map(async (course) => {

			return `<div class="col-6 p-0">
						<input type="checkbox" class="mr-2" name=checkbox value="${course.name}">
						<label class="form_title">${course.name}</label>
					</div>`;
		}));
		wishlists.innerHTML += wishlisthtml.join('');
		wishlists.innerHTML += `<div class="col-6 p-0">
									<input type="checkbox" class="mr-2" name=checkbox value="other">
									<label class="form_title">Khác</label>
								</div>`;
	});
	
}

$("#consultationRequestForm").on("submit", function (event) {
	event.preventDefault();

	var wishlists = "<br>" + getCheckedValues();
	var json = {
		"contactName": $("#contactName").val().trim(),
		"email": $("#email").val().trim(),
		"phoneNumber": $("#phoneNumber").val().trim(),
		"message": $("#message").val().trim() + wishlists,
		"isResolved": false
	};

	$.ajax({
		url: `${decodeURIComponent(API_BASE_URL)}/consultationrequests`,
		type: 'POST',
		data: JSON.stringify(json),
		contentType: "application/json",
		//headers: {
		//	Authorization: `Bearer ${token}`
		//},
		success: function (response) {
			console.log('Server response:', response);
			alert("Thông tin của bạn đã được ghi nhận, chúng tôi sẽ liên lạc với bạn sớm nhất có thể!");
		},
		error: function (error) {
			console.log('Error:', error);
			alert(error.responseText);
		}
	});
})

function getCheckedValues() {
	let checkboxes = document.querySelectorAll("#wishlists input[type='checkbox']:checked");
	let values = Array.from(checkboxes).map(cb => "- " + cb.value);
	let result = values.join("<br>");
	return result;
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
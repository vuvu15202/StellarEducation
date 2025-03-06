const API_BASE_URL = getCookie("apiurl") || "http://localhost:5000/api";



// Init When Load page
const initPage = async () => {
    //document.getElementById('homeActive').className = "active";

    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);


// ---------------------------------- Call API ----------------------------------
async function getCourses() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/courses`);
}

async function getCategories() {
    const callApi = async (API_BASE_URL) => {
        return (await fetch(API_BASE_URL)).json();
    }
    return await callApi(`${decodeURIComponent(API_BASE_URL)}/Categories`);
}

async function getLecturers() {
	const callApi = async (API_BASE_URL) => {
		return (await fetch(API_BASE_URL)).json();
	}
	return await callApi(`${decodeURIComponent(API_BASE_URL)}/Courses/GetAllLecture`);
}


// -------------------------------------------------------------------------------

async function pushDataOnLoad() {
	const container = document.getElementById("top6courses");
	const wishlists = document.getElementById("wishlists");

	getCourses().then(async (projects) => {
		projects = projects.slice(0, 6);
		let html = await Promise.all(projects.map(async (project) => {

			return `<div class="col-lg-4 course_col mb-5">
						<div class="course">
							<div class="course_image border border-5 rounded">
								<img width="100%" src="${project.image}" alt="">
							</div>
							<div class="course_body bg-light" style="height:110px;">
								<h3 class="course_title"><a href="/Courses/Detail?courseId=${project.courseId}">${project.name}</a></h3>
								<div class="course_teacher">Giáo viên: ${project.lecturer.lastName} ${project.lecturer.firstName}</div>
								<div class="course_text d-none">
									<p>${project.description}</p>
								</div>
							</div>
							<div class="course_footer">
								<div class="course_footer_content d-flex flex-row align-items-center justify-content-start">
									<div class="course_info">
										<i class="fa fa-graduation-cap" aria-hidden="true"></i>
										<span>${project.numberEnrolled} đang học</span>
									</div>
									<div class="course_info d-none">
										<i class="fa fa-star" aria-hidden="true"></i>
										<span>5 đánh giá</span>
									</div>
									<div class="text-danger course_price ml-auto">${project.price != 0 ? '<button class="btn btn-danger" data-toggle="modal" data-target="#carouselModal">Liên hệ</button>' : 'Miễn phí'}</div>
								</div>
							</div>
						</div>
					</div>`;
		})); 
		container.innerHTML += html.join('');
		//render wishlist
		let wishlisthtml = await Promise.all(projects.map(async (project) => {

			return `<div class="col-6 p-0">
						<input type="checkbox" class="mr-2" name=checkbox value="${project.name}">
						<label style="font-size:15px;">${project.name}</label>
					</div>`;
		}));
		wishlists.innerHTML += wishlisthtml.join('');
		wishlists.innerHTML += `<div class="col-6 p-0">
									<input type="checkbox" class="mr-2" name=checkbox value="other">
									<label style="font-size:15px;">Khác</label>
								</div>`;

	});

	getCategories().then(async (projects) => {
		const courseSelectCreate = document.getElementById('categories');

		projects.forEach(category => {
			const option = document.createElement('option');
			option.value = category.categoryId;
			option.textContent = category.name;
			courseSelectCreate.appendChild(option);
		});
	});

	getLecturers().then(async (lecturers) => {
		const lecturersEle = document.getElementById('lecturers');

		let html = await Promise.all(lecturers.map(async (lecturer) => {
			return `<div><img class="w-100" src="${lecturer.image}" alt="Ảnh 1" data-name="${lecturer.userName}" data-info="${lecturer.description}"></div>`
		}));
		lecturersEle.innerHTML += html.join('');

		// Gán biến $slider để tham chiếu đến slider
		var $slider = $('.image-slider');

		$slider.slick({
			centerMode: true,
			centerPadding: '20px',
			slidesToShow: 3,
			autoplay: true,
			autoplaySpeed: 2000,
			dots: true,
			arrows: true,
			responsive: [
				{
					breakpoint: 768,
					settings: {
						slidesToShow: 1,
						centerPadding: '20px'
					}
				}
			]
		});

		// Sau khi slider khởi tạo, cập nhật thông tin ngay lần đầu
		setTimeout(function () {
			let currentElement = $('.slick-center img');
			let name = currentElement.data('name');
			let info = currentElement.data('info');

			$('#info-name').text(name);
			$('#info-description').text(info);
		}, 100);


		// Cập nhật thông tin khi ảnh ra chính giữa
		$slider.on('afterChange', function (event, slick, currentSlide) {
			let currentElement = $('.slick-center img');
			let name = currentElement.data('name');
			let info = currentElement.data('info');

			$('#info-name').text(name);
			$('#info-description').text(info);
		});

		// Cập nhật khi nhấp vào ảnh
		$('.image-slider div').click(function () {
			let index = $(this).attr("data-slick-index"); // Lấy index thực từ data-slick-index
			$slider.slick('slickGoTo', index);

			// Bật lại autoplay sau khi chọn ảnh
			$slider.slick('slickPlay');
		});

	});


	


}


$(document).on('click', '#search', function () {
	var courseName = $("#courseName").val();
	var categories = $("#categories").val();

	$.ajax({
		url: `${decodeURIComponent(API_BASE_URL)}/Dashboard/getStatistic?year=${year}` + `&courseId=${courseId}`,
		type: "get",
		headers: {
			"Authorization": "Bearer " + token,
		},
		contentType: "application/json",
		success: function (result, status, xhr) {
			renderStatistic(result.statistic)
		},
		error: function (xhr, status, error) {
			console.log(xhr)
		}
	});
})

$(document).on('click', '#carouselSubmit', function () {
	var wishlists = "<br>" + getCheckedValues();
	var json = {
		"contactName": $("#contactNameCarousel").val().trim(),
		"email": $("#emailCarousel").val().trim(),
		"phoneNumber": $("#phoneNumberCarousel").val().trim(),
		"message": $("#messageCarousel").val().trim() + wishlists,
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

$("#consultationRequestForm").on("submit", function (event) {
	event.preventDefault();
	
	var json = {
		"contactName": $("#contactName").val().trim(),
		"email": $("#email").val().trim(),
		"phoneNumber": $("#phoneNumber").val().trim(),
		"message": $("#message").val().trim(),
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


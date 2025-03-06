const url = 'http://localhost:5000/api/Customer';
const pageNo = new URL(window.location.href).searchParams.get('pageNo') || 1;
const type = new URL(window.location.href).searchParams.get('type') || "";

// Init When Load page
const initPage = async () => {
    document.getElementById('projectsActive').className = 'active';

    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
async function getProjectsPagination(pageNo, type) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    if (type === "")
        return await callApi(`${url}/projects/pagination/${pageNo}`);
    else
        return await callApi(`${url}/projects/pagination/${pageNo}/${type}`);
}

async function getTotalAmountProject(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/projects/${id}/amount`);
}

async function getTotalType() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/projects/type`);
}

// -------------------------------------------------------------------------------
async function pushDataOnLoad() {
    const container = document.getElementById("listProjects");
    const paginationContainer = document.getElementById("paginationList");
    const typeContainer = document.getElementById("typeList");

    getProjectsPagination(pageNo, type).then(async (projects) => {
        let html = await Promise.all(projects.projects.map(async (project) => {
            const [amountData] = await Promise.all([getTotalAmountProject(project.projectId)])
            return `<div class="col-lg-6 course_col">
							<div class="course">
								<div class="course_image"><img src="${project.image}" alt=""></div>
								<div class="course_body">
									<h3 class="course_title"><a href="/project?id=${project.projectId}">${project.title}</a></h3>
									<div class="course_teacher">JG Organization</div>
									<div class="course_text">
										<p>${amountData.totalAmount.toLocaleString('vi', { style: 'currency', currency: 'VND' })} / ${project.targetAmount.toLocaleString('vi', { style: 'currency', currency: 'VND' })}</p>
									</div>
								</div>
								<div class="course_footer">
									<div class="course_footer_content d-flex flex-row align-items-center justify-content-start">
										<div class="course_info">
											<i class="fa fa-graduation-cap" aria-hidden="true"></i>
											<span>${amountData.totalDonate} donates</span>
										</div>
										<div class="course_info">
											<i class="fa fa-star" aria-hidden="true"></i>
											<span>${project.likeCount}</span>
										</div>
									</div>
								</div>
							</div>
						</div>`;
        }));
        let paginationHtml = '';
        if (pageNo <= projects.totalPage && pageNo >= 0) {
            if (projects.totalPage > 1) {
                if (projects.pageNo !== 1)
                    paginationHtml += `<li><a href="/projects?pageNo=${projects.pageNo - 1}&type=${type}"><i class="fa fa-angle-left" aria-hidden="true"></i></a></li>`;
                for (let i = 1; i <= projects.totalPage; i++)
                    if (i === projects.pageNo)
                        paginationHtml += `<li class="active"><a href="/projects?pageNo=${i}&type=${type}">${i}</a></li>`
                    else paginationHtml += `<li><a href="/projects?pageNo=${i}&type=${type}">${i}</a></li>`
                if (projects.pageNo !== projects.totalPage)
                    paginationHtml += `<li><a href="/projects?pageNo=${projects.pageNo + 1}&type=${type}"><i class="fa fa-angle-right" aria-hidden="true"></i></a></li>`;
            }
        }
        container.innerHTML += html.join('');
        paginationContainer.innerHTML = paginationHtml;
    })

    getTotalType().then(types => {
        let html = types.map(type => {
            return `<li><a href="https://localhost:5000/projects?pageNo=1&type=${type.type}">${type.text}</a></li>`
        })
        typeContainer.innerHTML += html.join('');
    })
}

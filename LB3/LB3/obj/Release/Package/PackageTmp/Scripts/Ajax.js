//const SERVER = "http://localhost:10000/LB3/";
//const SERVER = "http://localhost:59089/";
const SERVER = "http://172.16.193.234:10720/LB3/";

const CONTROLLER_DEFAULT_PATH = SERVER + 'api/students.json/';
const CONTROLLER_DEFAULT_XML_PATH = SERVER + 'api/students.xml/';

let pagesNumber = 0;
let StudentList = [];

let curOffset = 0;
let curLimit = 5;

let updateForm = $("#update-form");
let addForm = $("#add");
let searchForm = $("#search");

let isXml = () => $("#xml").prop("checked");

let testRequestParams;

function addStudentHandler() {

    addForm.find("button").on('click', () => {
        let requestParams;

        if (isXml()) {
            requestParams = {
                url: CONTROLLER_DEFAULT_XML_PATH,
                type: 'POST',
                headers: { 'Accept': 'application/xml' },
                data: addForm.serialize()
            };
        }

        else {
            requestParams = {
                url: CONTROLLER_DEFAULT_PATH,
                type: 'POST',
                headers: { 'Accept': 'application/json' },
                data: addForm.serialize()
            };
        }

        $.ajax(requestParams)
            .done((res) => {
                if (isXml()) { res = parser.parse(res).Root; }

                let studRes = 'Student ' + res.NAME + ' successfuly added! ID - #' + res.ID;
                showAlert("success", studRes);
            })

            .fail((jqXhr, textStatus, errorThrown) => {
                showAlert('danger', errorThrown);
            });
    });
}

function removeStudent(id) {
    let requestParams;

    if (isXml()) {
        requestParams = {
            url: (CONTROLLER_DEFAULT_XML_PATH + id),
            type: 'delete'
        };
    }
    else {
        requestParams = {
            url: (CONTROLLER_DEFAULT_PATH + id),
            type: 'delete'
        };
    }

    $.ajax(requestParams)
        .done((res) => {
            if (isXml()) { res = parser.parse(res).Root; }
            
            showAlert('info', 'Student ' + res.NAME + ' deleted successfuly.');
            getAllStudents();
        })

        .fail((jqXhr, textStatus, errorThrown) => {
            showAlert('danger', errorThrown);
        });

}

function searchStudentsHandler() {

    searchForm.find("button").on('click', () => {
        searchStudents(false);
    });

}

function searchStudents(all) {
    let requestParams;

    if (isXml()) {
        requestParams = {
            url: `${CONTROLLER_DEFAULT_XML_PATH}?${searchForm.serialize()}`,
            type: 'get',
            headers: { 'Accept': 'application/xml' }
        };
    }
    else {
        requestParams = {
            url: `${CONTROLLER_DEFAULT_PATH}?${searchForm.serialize()}`,
            type: 'get',
            headers: { 'Accept': 'application/json' }
        };
    }

    $.ajax(requestParams)
        .done((res) => {
            if (isXml()) {
                res = parser.parse(new XMLSerializer().serializeToString(res.documentElement)).ArrayOfStudent.Student;
            }
            
            StudentList = res;
            updateStudentList(StudentList);
        })

        .fail((jqXhr, textStatus, errorThrown) => {
            showAlert('danger', errorThrown);
        });
}

function showAlert(type, text) {

    if (type == '' || type == undefined)
        type = 'info';

    $('#alert-text').text(text);
    
    $('#alert').toggleClass('alert-' + type);

    $('#alert').fadeIn().delay(5500).fadeOut();
}

function updateStudentList(students) {

    $('#student-list > tbody').empty();

    for (let index in students) {

        let studRow;

        let stud = students[index];

        let removeBtn;
        let editBtn;

        let resLink = "";

        if ((typeof stud === "number") && isXml()) {

            removeBtn = '<button onclick="removeStudent(' + students.ID + ')" class="btn btn-danger">Remove #' + students.ID + '</button>';
            editBtn = '<button onclick="showEditModal(' + students.ID + ')" class="btn btn-info">Edit</button>';

            resLink = '<a href="'
                + students._links.self + '">student.xml/'
                + stud + '</a>';

            studRow = '<tr id="student-'
                + students.ID + '"><td>'
                + (students.ID == -1 ? "" : students.ID) + '</td><td>'
                + students.NAME + '</td><td>'
                + students.PHONE + '</td><td>'
                + removeBtn + ' ' + editBtn + '</td><td>'
                + resLink + '</td><tr>';
        }

        else if (isXml() && (typeof stud === "object")) {

            removeBtn = '<button onclick="removeStudent(' + stud.ID + ')" class="btn btn-danger">Remove #' + stud.ID + '</button>';
            editBtn = '<button onclick="showEditModal(' + index + ')" class="btn btn-info">Edit</button>';

            resLink = '<a href="'
                + stud._links.self + '">student.xml/'
                + stud.ID + '</a>';

            studRow = '<tr id="student-'
                + stud.ID + '"><td>'
                + (stud.ID == -1 ? "" : stud.ID) + '</td><td>'
                + stud.NAME + '</td><td>'
                + stud.PHONE + '</td><td>'
                + removeBtn + ' ' + editBtn + '</td><td>'
                + resLink + '</td><tr>';
        }

        else {

            removeBtn = '<button onclick="removeStudent(' + stud.ID + ')" class="btn btn-danger">Remove #' + stud.ID + '</button>';
            editBtn = '<button onclick="showEditModal(' + index + ')" class="btn btn-info">Edit</button>';

            resLink = '<a href="'
                + stud._links.self + '">student.json/'
                + stud.ID + '</a>';

            studRow = '<tr id="student-'
                + stud.ID + '"><td>'
                + (stud.ID == -1 ? "" : stud.ID) + '</td><td>'
                + stud.NAME + '</td><td>'
                + stud.PHONE + '</td><td>'
                + removeBtn + ' ' + editBtn + '</td><td>'
                + resLink + '</td><tr>';
        }

        $('#student-list > tbody').append(studRow);
    }
}

function updateStudent(studId) {
    let requestParams;

    if (isXml()) {
        requestParams = {
            url: CONTROLLER_DEFAULT_XML_PATH + studId,
            type: 'put',
            headers: { 'Accept': 'application/xml' },
            data: updateForm.serializeArray()
        };
    }
    else {
        requestParams = {
            url: CONTROLLER_DEFAULT_PATH + studId,
            type: 'put',
            headers: { 'Accept': 'application/json' },
            data: updateForm.serializeArray()
        };
    }

    $.ajax(requestParams)
        .done((res) => {
            if (isXml()) { res = parser.parse(res).Root; }

            let studRes = 'Student successfuly updated!' + res.ID;
            showAlert("success", studRes);
        })

        .fail((jqXhr, textStatus, errorThrown) => {
            showAlert('danger', errorThrown);
        });
}

function showEditModal(studIndex) {

    let student = StudentList[studIndex];

    $('#update-id').text(student.ID);
    $('#update-form input[name="id"]').val(student.ID);
    $('#update-form input[name="name"]').val(student.NAME);
    $('#update-form input[name="phone"]').val(student.PHONE);
    $('#update-modal').modal('toggle');

    updateForm.find("button").on('click', () => {
        updateStudent(student.ID);
        $('#update-modal').modal('toggle');
    });

}


function offsetAndLimitOnChange() {

    curOffset = searchForm.find('input[name="offset"]').val();
    curLimit = searchForm.find('input[name="limit"]').val();

}

function initForms() {

    addStudentHandler();
    searchStudentsHandler();
    searchForm.find('input[name="offset"], input[name="limit"]')
        .on('change', offsetAndLimitOnChange);

    searchStudents(true);
}


(function () { initForms(); }) ();
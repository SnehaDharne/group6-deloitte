
//navbar function to signout

function signoutbtn() {
    var new_xhr = new XMLHttpRequest();
    new_xhr.withCredentials = true;

    new_xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            var sess_user = this.responseText;
            var del_xhr = new XMLHttpRequest();
            del_xhr.withCredentials = true;
            var url_del = "http://localhost:5000/api/Session/";
            url_del += sess_user;

            del_xhr.addEventListener("readystatechange", function () {
                if (this.readyState === 4) {
                    window.open("http://localhost:5000/login.html", "_self");
                }
            });

            del_xhr.open("DELETE", url_del);

            del_xhr.send();
        }
    });

    new_xhr.open("GET", "http://localhost:5000/api/Session/GetId");

    new_xhr.send();

}


//check logged in on load
function checkLoggedIn() {
    var settingsUserId = {
        "url": "http://localhost:5000/api/Session/GetId",
        "method": "GET",
        "timeout": 0,
        "async": false
    };
    $.ajax(settingsUserId).done(function (response) {
        
    }).fail(function (response) {
        if (response.status == '404') {
            alert("please log in");
            window.open("http://localhost:5000/login.html", "_self");
        }
    });

}


//alert message for all pages

function alertmessage(message, type, alert_id) {
    var local_id = "#" + alert_id;
    $(local_id).fadeIn();
    const wrapper = document.createElement('div');
    wrapper.innerHTML = [
        `<div class="alert alert-${type} alert-dismissible text-center mx-auto mt-3 "style="width:fit-content; height:fit-content;" role="alert">`,
        `   <div class="mx-auto text-center">${message}</div>`,

        '</div>'
    ].join('');
    document.getElementById(alert_id).innerHTML = wrapper.innerHTML;
    $(local_id).delay(2000).fadeOut();
}


//home.html


var total_length;
var cur_userid;


//an onload function
function showFiles() {
    var i = 0;

    // Get current User Id
    var settingsCurUserId = {
        "url": "http://localhost:5000/api/Session/GetId",
        "method": "GET",
        "timeout": 0,
        "async": false
    };
    $.ajax(settingsCurUserId).done(function (response) {
        cur_userid = response;
    }).fail(function (response) {
        alert("fail");
    });
    //Get current user name
    //getting session details
    var name, firstname, lastname;
    var settingsFName = {
        "url": "http://localhost:5000/api/Session/GetfirstName",
        "method": "GET",
        "timeout": 0,
        "async": false
    };

    $.ajax(settingsFName).done(function (response) {
        firstname = response;
    }).fail(function (response) {
        alert("fail");
    });


    var settingsLName = {
        "url": "http://localhost:5000/api/Session/GetlastName",
        "method": "GET",
        "timeout": 0,
        "async": false
    };

    $.ajax(settingsLName).done(function (response) {
        lastname = response;
    }).fail(function (response) {
        alert("fail");
    });

    name = "Welcome " + firstname + " " + lastname;

    document.getElementById("welcome_user").innerHTML = name;


    // Get a list of names of the files uploaded by the current user
    var listOfFileNames;
    url_FileNames = "http://localhost:5000/api/Metadata/GetMetadataFileNames/?username=" + cur_userid;
    var settingsFN = {
        "url": url_FileNames,
        "method": "GET",
        "timeout": 0,
        "async": false
    };
    $.ajax(settingsFN).done(function (response) {
        listOfFileNames = response;
        total_length = response.length;
    });

    // Get a list of Ids of the files uploaded by the current user
    var listOfIds;
    url_Ids = "http://localhost:5000/api/Metadata/GetMetadataIds/?username=" + cur_userid;
    var settingsFN = {
        "url": url_Ids,
        "method": "GET",
        "timeout": 0,
        "async": false
    };
    $.ajax(settingsFN).done(function (response) {
        listOfIds = response;
    });

    for (i = 0; i < total_length; i++) {
        var listitem = '<button class= "list-group-item list-group-item-action" id = "'
        listitem += listOfIds[i];
        listitem += '" type = "button" onclick ="showDetails(this)">';
        listitem += listOfFileNames[i];
        listitem += '</button>';
        document.getElementById("add_after_me").insertAdjacentHTML("afterend", listitem);
    }
}


//home check loggedein
function checkLoggedInHome() {
    var settingsUserId = {
        "url": "http://localhost:5000/api/Session/GetId",
        "method": "GET",
        "timeout": 0,
        "async": false
    };
    $.ajax(settingsUserId).done(function (response) {
            showFiles();
    }).fail(function (response) {
        if (response.status == '404') {
            alert("please log in");
            window.open("http://localhost:5000/login.html", "_self");
        } else {
            alert("Something went wrong!");
        }
    });

}
//a clickable function
function showDetails(ele) {
    var smt = ele.id;

    //redundunt-region
    // Get a list of Ids of the files uploaded by the current user
    var listOfIds;

    url_Ids = "http://localhost:5000/api/Metadata/GetMetadataIds/?username=" + cur_userid;
    var settingsFN = {
        "url": url_Ids,
        "method": "GET",
        "timeout": 0,
        "async": false
    };
    $.ajax(settingsFN).done(function (response) {
        listOfIds = response;
    });
    // redundant region stops here

    for (i = 0; i < total_length; i++) {
        cur_id = listOfIds[i].toString();
        document.getElementById(cur_id).style.backgroundColor = 'white';
        document.getElementById(cur_id).style.color = 'black';
    }

    // change color of this button to blue
    document.getElementById(smt).style.backgroundColor = '#0184fc';
    document.getElementById(smt).style.color = 'white';

    //showing details
    var url1 = "http://localhost:5000/api/Metadata/";
    url1 += smt;

    var settings = {
        "url": url1,
        "method": "GET",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {

        document.getElementById("file_nametxt").innerHTML = response[2];
        document.getElementById("created_bytxt").innerHTML = response[0];
        document.getElementById("created_ontxt").innerHTML = response[1];
    });

}
//a clickable function
function Download() {
    var fn = document.getElementById("file_nametxt").innerHTML.toString();
    var url_downl = "http://localhost:5000/api/File/Download?fileName=";
    url_downl += fn;

    var requestOptions = {
        method: 'GET',
        redirect: 'follow',
        async: false
    };

    fetch(url_downl, requestOptions)
        .then(response => response.blob())
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            // the filename you want
            a.download = fn;
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
        });
}



//upload.html

 function uploadfile() {
        var form = new FormData();
        const fileInput = document.getElementById('fileUpload');
        if (fileInput.files[0].name.endsWith(".pbix")) {
            var currentdate = new Date();
            var datetime = currentdate.getDate() + "/"
                + (currentdate.getMonth() + 1) + "/"
                + currentdate.getFullYear();

            //getting session details
            var name, firstname, lastname, userid;
            var settingsFName = {
                "url": "http://localhost:5000/api/Session/GetfirstName",
                "method": "GET",
                "timeout": 0,
                "async": false
            };

            $.ajax(settingsFName).done(function (response) {
                firstname = response;
            }).fail(function (response) {
                alert("fail");
            });

            var settingsLName = {
                "url": "http://localhost:5000/api/Session/GetlastName",
                "method": "GET",
                "timeout": 0,
                "async": false
            };

            $.ajax(settingsLName).done(function (response) {
                lastname = response;
            }).fail(function (response) {
                alert("fail");
            });

            name = firstname + " " + lastname;

            var settingsUserId = {
                "url": "http://localhost:5000/api/Session/GetId",
                "method": "GET",
                "timeout": 0,
                "async": false
            };
            $.ajax(settingsUserId).done(function (response) {
                userid = response;
            }).fail(function (response) {
                alert("fail");
            });

            // Metadata push
            var data = JSON.stringify({
                "file_name": fileInput.files[0].name,
                "created_on": datetime,
                "created_by": name,
                "userid": userid
            });

            var xhr = new XMLHttpRequest();
            xhr.withCredentials = true;

            xhr.addEventListener("readystatechange", function () {
                if (this.readyState === 4) {
                    console.log(this.responseText);
                }
            });

            xhr.open("POST", "http://localhost:5000/api/Metadata", false);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
            xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
            xhr.send(data);

            // File Upload
            var upload_data = new FormData();
            upload_data.append("formFiles", fileInput.files[0], fileInput.files[0].name);

            var upload_xhr = new XMLHttpRequest();
            upload_xhr.withCredentials = true;

            upload_xhr.addEventListener("readystatechange", function () {
                if (this.readyState === 4) {
                    console.log(this.responseText);
                    alertmessage("Uploaded Sucessfully!", 'success', 'alert_upload');
                    document.getElementById("fileUpload").value = null;
                }
            });

            upload_xhr.open("POST", "http://localhost:5000/api/File/Upload", false);
            upload_xhr.send(upload_data);
        } else {

            alertmessage("Upload failure! Only pbix file type is supported.", 'danger', 'alert_upload');
            document.getElementById("fileUpload").value = null;
        }
    }


//user-management



function Update() {
    var userid;
    //getting session details
    var settings = {
        "url": "http://localhost:5000/api/Session/GetId",
        "method": "GET",
        "timeout": 0,
        async: false,
    };

    $.ajax(settings).done(function (response) {
        userid = response;
    }).fail(function (response) {
        alert("fail");
    });


    var contact_id = document.getElementById("contact").value;
    var firstName = document.getElementById("fName").value;
    var lastName = document.getElementById("lName").value;
    var password = document.getElementById("passw").value;
    var contact_id_is_empty;

    if (!firstName && !lastName && !password && !contact_id) {
        alertmessage('Enter at least one field to update.', 'warning', 'alert_user_management');
    }
    else {
        if (!contact_id) {
            // Handle empty contact input for non update as it is not possible
            // to compare ulong type with empty string  
            contact_id = "0";
            contact_id_is_empty = true;
        }



        if (isNaN(parseInt(contact_id))) {
            // User enters text for contact
            alertmessage("Contact must be a number.", 'warning', 'alert_user_management');
        } else if (contact_id.length != 10 && !contact_id_is_empty) {
            // User inputs a non 10 digit number
            alertmessage("Contact must be 10 digits long.", 'warning', 'alert_user_management');
            // alert("10 digs");
        } else {
            var data = JSON.stringify({
                "id": userid,
                "firstName": firstName,
                "lastName": lastName,
                "contact": parseInt(contact_id),
                "password": password,
                "isDeleted": false
            });
            var xhr = new XMLHttpRequest();
            xhr.withCredentials = true;

            xhr.addEventListener("readystatechange", function () {
                if (this.readyState === 4) {
                    console.log(this.responseText);
                    var firstname, lastname, name;

                    //getting name from User table to update metadata table information
                    urlFName = "http://localhost:5000/api/User/GetfirstName" + "?username=" + userid;
                    var settingsFName = {
                        "url": urlFName,
                        "method": "GET",
                        "timeout": 0,
                        async: false,
                    };

                    $.ajax(settingsFName).done(function (response) {
                        firstname = response;
                    }).fail(function (response) {
                        alert("fail");
                    });

                    urlLName = "http://localhost:5000/api/User/GetlastName" + "?username=" + userid
                    var settingsLName = {
                        "url": urlLName,
                        "method": "GET",
                        "timeout": 0,
                        async: false,
                    };

                    $.ajax(settingsLName).done(function (response) {
                        lastname = response;
                    }).fail(function (response) {
                        alert("fail");
                    });

                    name = firstname + " " + lastname;

                    //Updating all metadata names
                    md_url = "http://localhost:5000/api/Metadata/" + userid + "?fullname=" + name;
                    var settings = {
                        "url": md_url,
                        "method": "PUT",
                        "timeout": 0,
                        async: false,
                    };

                    $.ajax(settings).done(function (response) {
                        signoutbtn();
                    }).fail(function (response) {
                        alert("fail");
                    });

                }
            });

            xhr.open("PUT", "http://localhost:5000/api/User");
            xhr.setRequestHeader("Content-Type", "application/json");

            xhr.send(data);

        }
    }
}

//signup

function Create() {
    var id, firstName, lastName, contact, password;
    // Email is not case sensitive, so it should always be stored in lowercase
    id = document.getElementById("floatingInputEmail").value.toLowerCase();
    firstName = document.getElementById("floatingInputfName").value;
    lastName = document.getElementById("floatingInputlName").value;
    contact = document.getElementById("floatingInputContact").value;
    password = document.getElementById("floatingPassword").value;

    // User leaves field empty
    if (!id || !firstName || !lastName || !contact || !password) {
        alertmessage("Please enter all details.", 'warning', 'alert_signup');
    } else if (isNaN(parseInt(contact))) {
        // User enters text for contact
        alertmessage("Contact must be a number.", 'warning', 'alert_signup');
    } else if (contact.length != 10) {
        // User inputs a non 10 digit number
        alertmessage("Contact must be 10 digits long.", 'warning', 'alert_signup');
    } else {
        // WARNING: For POST requests, body is set to null by browsers.
        var data = JSON.stringify({
            "id": id,
            "firstName": firstName,
            "lastName": lastName,
            "contact": parseInt(contact),
            "password": password,
            "isDeleted": false
        });

        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener("readystatechange", function () {
            if (this.readyState === 4) {
                if (xhr.status == "200") {
                    window.open("http://localhost:5000/login.html", "_self");
                } else if (xhr.status == "500") { // Primary Key cannot duplicate
                    alertmessage("A user with this email already exists.", 'warning', 'alert_signup');
                } else if (xhr.status == "400") {
                    alertmessage("Invalid email format.", 'warning', 'alert_signup');
                } else {
                    alertmessage("Something went wrong.", 'warning', 'alert_signup');
                }
            }
        });

        xhr.open("POST", "http://localhost:5000/api/user", false);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
        xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
        xhr.send(data);
    }

}


//login

function signIn() {
    var email = document.getElementById("floatingInput").value.toLowerCase();
    var pw = document.getElementById("floatingPassword").value;

    if (!email) {
        alertmessage("Please enter your email.", 'warning', 'alert_login');
    } else if (!pw) {
        alertmessage("Please enter your password.", 'warning', 'alert_login');
    } else {
        var str = "http://localhost:5000/api/User/";
        str += email;
        str += '/';
        str += pw;
        var settings = {
            "url": str,
            "method": "get",
            "timeout": 0,
            async: false,
        };
        $.ajax(settings).done(function (response) {
            var data = JSON.stringify(response);
            var xhr = new XMLHttpRequest();
            xhr.withCredentials = true;
            xhr.addEventListener("readystatechange", function () {
                if (this.readyState === 4) {
                    console.log(this.responseText);

                    window.open("http://localhost:5000/home.html", "_self");
                }

            });
            xhr.open("POST", "http://localhost:5000/api/Session");
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
            xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
            xhr.send(data);
        }).fail(function (response) {
            // 404 - Not Found - User doesn't exist
            // 400 - Bad Request - Wrong Password
            if (response.status == "404") {
                alertmessage("Account doesn't exist in the database. Please check details.", 'warning', 'alert_login');
            }
            else if (response.status == "400") {
                alertmessage("Incorrect password entered.", 'danger', 'alert_login');
            } else {
                alertmessage("Something went wrong.", 'warning', 'alert_login');
            }
        });
    }

}

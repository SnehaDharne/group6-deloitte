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
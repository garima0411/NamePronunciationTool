async function recordNamePronouciation() {
            const employeeId = document.getElementById("employeeId").textContent;


            const endpoint = new URL("http://127.0.0.1:5000/recording?employeeId=" + employeeId);
            var headers = { };
            const response = await fetch(endpoint,
                {
        method: "GET",
                    mode: 'no-cors',
                    headers: headers

                });

        }
        async function getNamePronouciation() {


            const username = document.getElementById("idName").innerHTML;
            const employeeId = document.getElementById("employeeId").textContent;
            let endpoint = "";

            endpoint = new URL("http://127.0.0.1:5000/users?employeeId=" + employeeId);
            getData(endpoint)
                .then(data => {
        console.log(data);
                    document.getElementById('speechToText').innerHTML = data; //KeysinAudio

                });


        }
        document.getElementById("idTrendspan").innerHTML = document.getElementById("idName").innerHTML;
        document.getElementById("idA").addEventListener("click", writeNameIntoTex.bind(event, "A"), false);
        document.getElementById("idB").addEventListener("click", writeNameIntoTex.bind(event, "B"), false);
        document.getElementById("idC").addEventListener("click", writeNameIntoTex.bind(event, "C"), false);
        document.getElementById("idD").addEventListener("click", writeNameIntoTex.bind(event, "D"), false);
        document.getElementById("idE").addEventListener("click", writeNameIntoTex.bind(event, "E"), false);
        document.getElementById("idF").addEventListener("click", writeNameIntoTex.bind(event, "F"), false);
        document.getElementById("idG").addEventListener("click", writeNameIntoTex.bind(event, "G"), false);
        document.getElementById("idH").addEventListener("click", writeNameIntoTex.bind(event, "H"), false);
        document.getElementById("idI").addEventListener("click", writeNameIntoTex.bind(event, "I"), false);
        document.getElementById("idJ").addEventListener("click", writeNameIntoTex.bind(event, "J"), false);
        document.getElementById("idK").addEventListener("click", writeNameIntoTex.bind(event, "K"), false);
        document.getElementById("idL").addEventListener("click", writeNameIntoTex.bind(event, "L"), false);
        document.getElementById("idM").addEventListener("click", writeNameIntoTex.bind(event, "M"), false);
        document.getElementById("idN").addEventListener("click", writeNameIntoTex.bind(event, "N"), false);
        document.getElementById("idO").addEventListener("click", writeNameIntoTex.bind(event, "O"), false);
        document.getElementById("idP").addEventListener("click", writeNameIntoTex.bind(event, "P"), false);
        document.getElementById("idQ").addEventListener("click", writeNameIntoTex.bind(event, "Q"), false);
        document.getElementById("idR").addEventListener("click", writeNameIntoTex.bind(event, "R"), false);
        document.getElementById("idS").addEventListener("click", writeNameIntoTex.bind(event, "S"), false);
        document.getElementById("idT").addEventListener("click", writeNameIntoTex.bind(event, "T"), false);
        document.getElementById("idU").addEventListener("click", writeNameIntoTex.bind(event, "U"), false);
        document.getElementById("idV").addEventListener("click", writeNameIntoTex.bind(event, "V"), false);
        document.getElementById("idW").addEventListener("click", writeNameIntoTex.bind(event, "W"), false);
        document.getElementById("idX").addEventListener("click", writeNameIntoTex.bind(event, "X"), false);
        document.getElementById("idY").addEventListener("click", writeNameIntoTex.bind(event, "Y"), false);
        document.getElementById("idZ").addEventListener("click", writeNameIntoTex.bind(event, "Z"), false);
        document.getElementById("idSpace").addEventListener("click", writeNameIntoTex.bind(event, " "), false);

        document.getElementById("idBackspace").addEventListener("click", deleteNameIntoTex);
        document.getElementById("idClear").addEventListener("click", clearNameIntoTex);


        function writeNameIntoTex(str = '') {
        let val = document.getElementById("idSigntxt").innerHTML;
            document.getElementById("idSigntxt").innerHTML = val + str;
        }
        function deleteNameIntoTex() {
        let letters = document.getElementById("idSigntxt").innerHTML;
            let strLetters = letters.split("");
            strLetters.pop();
            let text = strLetters.join("");
            document.getElementById("idSigntxt").innerHTML = text;
        }
        function clearNameIntoTex() {

        document.getElementById("idSigntxt").innerHTML = '';
        }


        async function getSignPronouciation() {


            const username = document.getElementById("idName").innerHTML;
            const employeeId = document.getElementById("employeeId").textContent;
            let endpoint = new URL("http://127.0.0.1:5000/usersSignLang?employeeId=" + employeeId);

            getData(endpoint)
                .then(data => {
        console.log(data);
                    let myArray = new Array();
                    myArray = data.split("");
                    document.getElementById("usrspeechToText").style.fontSize = "large";
                    document.getElementById('usrspeechToText').innerHTML = data.toUpperCase(); //KeysinAudio

                    arr = myArray.filter(function (entry) { return entry.trim() != ''; });
                    play(arr);
                    function play(arr) {
                        var videoSource = new Array();
                        var videos = arr;
                        var j;
                        for (j = 0; j < videos.length; {
        videoSource[j] = "~/videos/" + videos[j].toUpperCase() + ".mp4";
                        }

                        var i = 0; // define i
                        var videoCount = videoSource.length;

                        function videoPlay(videoNum) {
        document.getElementById('KeysinAudio').innerHTML = videos[videoNum].toUpperCase(); //KeysinAudio
                            document.getElementById("KeysinAudio").style.color = "#09edc7";
                            document.getElementById("KeysinAudio").style.fontSize = "xx-large";

                            document.getElementById("videoPlayer").setAttribute("src", videoSource[videoNum]);
                            document.getElementById("videoPlayer").load();
                            document.getElementById("videoPlayer").play();

                        }
                        document.getElementById('videoPlayer').addEventListener('ended', myHandler, false);
                        document.getElementById('KeysinAudio').innerHTML = videos[0].toUpperCase(); //KeysinAudio
                        document.getElementById("KeysinAudio").style.color = "#09edc7";
                        document.getElementById("KeysinAudio").style.fontSize = "xx-large";
                        //document.getElementById("list").getElementsByTagName("li")[0].style.fontSize = "xx-large";

                        videoPlay(0); // play the video

                        function myHandler() {
        i++;
                            if (i == videoCount) {

        document.getElementById("videoPlayer").pause();
                            }
                            else {

        videoPlay(i);
                            }
                        }
                    }

                });


        }
        async function getData(url = '') {
            var data = ''
            // Default options are marked with *
            const response = await fetch(url, {
        method: 'GET',
                mode: 'cors',
                cache: 'no-cache',
                credentials: 'same-origin',
                headers: {
        'Content-Type': 'text/plain'
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                },
                redirect: 'follow'
            }).then(response => {
                return response.text()
            }).then(text => {
        console.log(text)
                data = text
            });
            return data;
        }

        function toggleFields() {
            var checkbox = document.getElementById('togBtn');
            if (checkbox.checked === true) {
        document.getElementById('divlanguage').style.display = 'none';
                document.getElementById('divvoice').style.display = 'none';
                document.getElementById('slowDiv').style.display = 'none';
            }
            else {
        document.getElementById('divlanguage').style.display = 'inline-block';
                document.getElementById('divvoice').style.display = 'inline-block';
                document.getElementById('slowDiv').style.display = 'inline-block';

            }
        }
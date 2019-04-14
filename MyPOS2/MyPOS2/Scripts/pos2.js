
/* ========================================================================
 * Scripts javascript for 
 * MyPOS
 * ======================================================================== */

/*
 * Scripts for
 * Transaction views
* ======================================================================== */

function goBack() {
    window.history.go(-1);
}

function ButtonCalc_Click(id) {
    var val = id.getAttribute('Value');
    document.getElementById('addProduct').value += val;
}

function ButtonDelete_Click() {
    var val = document.getElementById('addProduct').value;
    document.getElementById('addProduct').value = val.substring(0, val.length - 1);
}

function ButtonAddProduct_Click() {
    var minus = false;
    CreateRquestAddOrRemove(minus);
}

function ButtonRemoveProduct_Click() {
    var minus = true;
    CreateRquestAddOrRemove(minus);
}

function CreateRquestAddOrRemove(minus) {
    document.getElementById('errorAddProduct').textContent = "";
    document.getElementById('errorAddProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('addProduct').value;
        if (val === null || val === "") {
            throw "Il faut saisir un produit";
        } else {
            var val = document.getElementById('addProduct').value;
            var numTransaction = document.getElementById('NumTransaction').value;
            var terminal = document.getElementById('TerminalId').value;

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById('addProduct').value = "";
                    document.getElementById('detail').innerHTML = xhr.responseText;
                    var disc = document.getElementById('globalDiscount').value;
                    if (disc === "" || disc === null || disc === undefined) {
                        document.getElementById('GlobalTot').value = document.getElementById('subTotal1').value;
                    } else {
                        AddDiscount();
                    }
                }
            }

            //Post Method
            var url = "/Transaction/RefreshDetails";
            var param = "addProduct=" + val
                + "&numTransaction=" + numTransaction
                + "&terminalId=" + terminal
                + "&minus=" + minus;
            xhr.open("POST", url);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(param);
        }
    } catch (e) {
        document.getElementById('errorAddProduct').textContent = e;
        document.getElementById('errorAddProduct').style.visibility = "visible";
        console.log(e);
    }
}

function AddDiscount() {
    document.getElementById('errorGlobalDiscount').textContent = "";
    document.getElementById('errorGlobalDiscount').style.visibility = "hidden";
    try {
        var subT = document.getElementById('subTotal1').value;
        subT = parseFloat(subT.replace(",", "."));
        var d = (parseFloat(document.getElementById('globalDiscount').value)) / 100;
        if (d < 0 || d > 1) {
            throw "valeur en % devant être comprise entre 0 et 100";
        } else if (Number.isNaN(d) || d == undefined || d == null || d === "") {
            throw "valeur devant être un nombre entre 0 et 100";
        } else if (d == 0) {
            document.getElementById('GlobalTot').value = subT;
        } else {
            var result = parseFloat(subT - (subT * d)).toFixed(2);
            document.getElementById('GlobalTot').value = result;
        }
    }
    catch (e) {
        document.getElementById('errorGlobalDiscount').textContent = e;
        document.getElementById('errorGlobalDiscount').style.visibility = "visible";
        console.log(e);
    }
}

function SearchByCodeOrName() {
    document.getElementById('errorSearchProduct').textContent = "";
    document.getElementById('errorSearchProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('searchProduct').value;
        if (val === null || val === "") {
            throw "Il faut saisir un produit";
        } else {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById('containerRight').innerHTML = xhr.responseText;
                }
            }

            //Post Method
            var url = "/Transaction/SearchProduct";
            var param = "Product=" + val;
            xhr.open("POST", url);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(param);
        }
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function SearchAllProduct() {
    try {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('containerRight').innerHTML = xhr.responseText;
            }
        }

        //Get Method
        var url = "/Transaction/SearchAllProduct" 
	    xhr.open("GET", url);
	    xhr.send();


    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function AddItem(item) {
    document.getElementById('addProduct').value = item;
}

function SearchBy(method) {
    var val = method.getAttribute('Value');
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('containerRight').innerHTML = xhr.responseText;
        }
    }

    //Post Method
    var url = "/Transaction/SearchBy";
    var param = "Method=" + val;
    xhr.open("POST", url);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

function ProductBy(id, meth) {
    var val = id.getAttribute('Value');
    var method = meth;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('containerRight').innerHTML = xhr.responseText;
        }
    }

    //Post Method
    var url = "/Transaction/ProductBy";
    var param = "Method=" + method
        + "&Argument=" + val;
    xhr.open("POST", url);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

function SearchHostName() {
    try {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('nameId').innerHTML = xhr.responseText;
                var T = document.getElementById('detail').textContent;
                document.getElementById('nameTerminal').value = T.trim();
                
            }
        }

        //Get Method
        var url = "/Terminals/SearchHostName"
        xhr.open("GET", url);
        xhr.send();
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function ChangeLanguageTicket() {
    try {
        var val = document.getElementById('Language').value;
        var transac = document.getElementById('NumTransaction').value;
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('myTicket').innerHTML = xhr.responseText;
                

            }
        }

        //Post Method
        var url = "/Pay/ChangeLanguageTicket";
        var param = "Language=" + val
            + "&NumTransaction=" + transac;
        xhr.open("POST", url);
        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhr.send(param);
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function FctPrint(divName) {
    var printContents = document.getElementById(divName).innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;

    window.print();

    document.body.innerHTML = originalContents;
}

//function ImportImage() {
//    try {
//        var path = document.getElementById('newImageHero').value;
//        var filename = path.substring(path.lastIndexOf('\\') + 1);
//        var source = document.getElementById('sourceName').textContent;
//        var file = document.getElementById('newImageHero').files[0];
//        //find controller name
//        var c = window.location.pathname.split("/");
//        var controller = c[1];

//        var xhr = new XMLHttpRequest();
//        xhr.onreadystatechange = function () {
//            if (this.readyState == 4 && this.status == 200) {
//                document.getElementById('nameId').innerHTML = xhr.responseText;
//                var T = document.getElementById('detail').textContent;
//                document.getElementById('ImageHero').value = T.trim();

//            }
//        }
//        switch (source) {

//            default:
//        }

//        //Post Method
//        var url = "/" + controller +"/Import";
//        var param = "file=" + file
//            + "&filename=" + filename
//            + "&source=" + source;
//        xhr.open("POST", url);
//        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//        xhr.send(param);
//    } catch (e) {
//        document.getElementById('errorSearchProduct').textContent = e;
//        document.getElementById('errorSearchProduct').style.visibility = "visible";
//        console.log(e);
//    }
//}

function ImportImage() {
    try {
        //var path = document.getElementById('newImageHero').value;
        //var filename = path.substring(path.lastIndexOf('\\') + 1);
        var source = document.getElementById('sourceName').textContent;
        //var file = document.getElementById('newImageHero');
        //var dataString = new FormData(form);
        //var test = form;
        var formData = new FormData();
        //formData.append("filename", filename);
        //Find source
        switch (source.toLowerCase()) {
            case "marque":
                source = "brand";
                break;
            case "catégorie":
                source = "category";
                break;
            case "produit":
                source = "product";
                break;
            case "magasin":
                source = "shop";
                break;
            default:
                break;
        }
        formData.append("source", source);
        //Find file
        formData.append("file", $('#newImage')[0].files[0]);

        //find controller name
        var c = window.location.pathname.split("/");
        var controller = c[1];
        var url = "/" + controller + "/Import";

        $.ajax({
            url: url,
            type: 'Post',
            data: formData,
            success: function (result) {
                document.getElementById('nameId').innerHTML = result;
                var T = document.getElementById('detail').textContent;

                switch (source.toLowerCase()) {
                    case "hero":
                        if (document.getElementById('ImageHero') != null) {
                            document.getElementById('ImageHero').value = T.trim();
                        }
                        else {
                            document.getElementById('Hero_imageHero').value = T.trim();
                        }
                        break;

                    case "age":
                        if (document.getElementById('imageAge') != null) {
                            document.getElementById('imageAge').value = T.trim();
                        }
                        else {
                            //document.getElementById('Hero_imageHero').value = T.trim();
                        }
                        break;

                    case "brand":
                        if (document.getElementById('imageBrand') != null) {
                            document.getElementById('imageBrand').value = T.trim();
                        }
                        else {
                            //document.getElementById('Hero_imageHero').value = T.trim();
                        }
                        break;

                    case "shop":
                        if (document.getElementById('LogoShop') != null) {
                            document.getElementById('LogoShop').value = T.trim();
                        }
                        else {
                            document.getElementById('Shop_logoShop').value = T.trim();
                        }
                        break;

                    default:
                        break;
                }
                var test = (T.trim()).substring(1);  
                document.getElementById('imgLogo').src = (T.trim()).substring(1);        
            },
            error: function () { },
            cache: false,
            contentType: false,
            processData: false
        });

    } catch (e) {
        document.getElementById('errorImport').innerHTML = e.message;
        document.getElementById('errorImport').style.visibility = "visible";

        console.log(e);
    }
}

function validateFormTranslation() {
    try {
        //find controller name
        var c = window.location.pathname.split("/");
        var controller = c[1];
        var counter = document.getElementById('counter').textContent;
        switch (controller.toLowerCase()) {
            case "shops":
                var valName = document.getElementById('ShopsTrans_0__nameShop').value;
                if (valName == null || valName == "") {
                    throw "Le champ <Universel ou Français> est obligatoire pour le nom";
                }
                var valStreet = document.getElementById('ShopsTrans_0__street').value;
                if (valStreet == null || valStreet == "") {
                    throw "Le champ <Universel ou Français> est obligatoire pour la rue";
                }
                var valCity = document.getElementById('ShopsTrans_0__city').value;
                if (valCity == null || valCity == "") {
                    throw "Le champ <Universel ou Français> est obligatoire pour la ville";
                }
                var valOpening = document.getElementById('ShopsTrans_0__opening').value;
                if (valOpening == null || valOpening == "") {
                    throw "Le champ <Universel ou Français> est obligatoire pour les heures d'ouvertures";
                }
                break;
            case "categories":

                break;
            case "products":

                break;
            case "messages":

                break;
            default:
                break;
        }
        //need JQuery to send the form
        $("#theForm").submit();
    } catch (e) {
        document.getElementById('errorValidation').textContent = e;
        document.getElementById('errorValidation').style.visibility = "visible";
        console.log(e);
    }

    
}

function DaylyReports() {
    var chkBox = document.getElementById('Dayly');
    if (chkBox.checked == true) {
        document.getElementById('dateReportBox').style.visibility = "visible";
        document.getElementById('Monthly').disabled = true;
    }
    else {
        document.getElementById('Date').value = "";
        document.getElementById('dateReportBox').style.visibility = "hidden";
        document.getElementById('Monthly').disabled = false;
    }

    
}

function MonthlyReports() {
    var chkBox = document.getElementById('Monthly');
    if (chkBox.checked == true) {
        document.getElementById('Dayly').disabled = true;
    }
    else {
        document.getElementById('Dayly').disabled = false;
    }
}

function CreateReport() {
    try {
        document.getElementById('errorFormReport').style.visibility = "hidden";
        var typeR = document.getElementById('TypeReportId').value;
        var chkBoxD = document.getElementById('Dayly');
        var chkBoxM = document.getElementById('Monthly');
        var date = document.getElementById('Date').value;
        if (typeR === null || typeR === "") {
            throw "Il faut choisir un type de rapport";
        } else if (chkBoxD.checked == true) {
            if (date === null || date === "") {
                throw "Il faut choisir une date pour le rapport journalier";
            }
        }

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('myReport').innerHTML = xhr.responseText;
                document.getElementById('myReport').style.visibility = 'visible';
                switch (typeR) {
                    case "1":
                        if (chkBoxD.checked == true) {
                            document.getElementById("reportTitle").innerHTML = "Rapport journalier des ventes totales";
                        } else if (chkBoxM.checked == true) {
                            document.getElementById("reportTitle").innerHTML = "Rapport mensuel des ventes totales";
                        } else {
                            document.getElementById("reportTitle").innerHTML = "Rapport des ventes totales";
                        }
                        break;
                    case "2":
                        if (chkBoxD.checked == true) {
                            document.getElementById('reportTitle').innerHTML = "Rapport journalier des ventes totales par article";
                        } else if (chkBoxM.checked == true) {
                            document.getElementById('reportTitle').innerHTML = "Rapport mensuel des ventes totales par article";
                        } else {
                            document.getElementById('reportTitle').innerHTML = "Rapport des ventes totales par article";
                        }
                        break;

                    default:
                        break;
                }
                
            }
        }

        //Post Method
        var url = "/Reports/Index";
        var param = "TypeReportId=" + typeR
            +"&Dayly=" + chkBoxD.checked
            + "&Monthly=" + chkBoxM.checked
            + "&Date=" + date;
        xhr.open("POST", url);
        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhr.send(param);

    } catch (e) {
        document.getElementById('errorFormReport').textContent = e;
        document.getElementById('errorFormReport').style.visibility = "visible";
        console.log(e);
    }
}
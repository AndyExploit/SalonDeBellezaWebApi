var indice = -1;
function add() {
    var slServicio = document.getElementById("exampleFormControlSelect1");
    var idServicio = slServicio.value;
    var opcionSelectServicio = slServicio.options[slServicio.selectedIndex];
    var servicio = opcionSelectServicio.text;
    var precio = opcionSelectServicio.getAttribute("data-precio");
    var duracion = opcionSelectServicio.getAttribute("data-duracion");
    indice++
    var t1 = document.getElementById("table");
    let tbody = document.getElementById("tbd");

    let rowl = document.createElement("tr");
    //row.heigth - 110
    for (var i = 0; i < 5; i++) {
        var col1 = document.createElement("TD");
        /*col1.innerHTML = "<font color='black'>" + "Prueba" + "</font>";*/
        if (i >= 0 || i <= 1) {
            var txt = document.createElement("input");
            col1.appendChild(txt);
            txt.type = "hidden";
            if (i == 0) {
                // col1.innerHTML = "<font color='black'>" + ("Prueba " + id) + "</font>";
                col1.style.display = "none";
                col1.innerHTML = "<font color='black'>0</font>";
                txt.name = "DetalleCita[" + indice + "].Id";
                txt.setAttribute("data-name", "DetalleCita[{0}].Id");
                txt.value = 0;              
            }
            else if (i == 1) {
                txt.type = "text";
                col1.style.display = "none";
                txt.name = "DetalleCita[" + indice + "].IdServicio";
                txt.setAttribute("data-name", "DetalleCita[{0}].IdServicio");
                txt.value = idServicio;              
            }
            else if (i == 2) {              
               // txt.name = "DetalleCita[" + indice + "].Servicio.Nombre";
               // txt.setAttribute("data-name", "DetalleCita[{0}].Servicio.Nombre");
               // txt.value = servicio;
                col1.innerText = servicio;
                //var span = document.createElement("span");
              //  span.innerText = servicio;               
                //col1.appendChild(span);
            }
            else if (i == 3) {
                txt.type = "text";
                txt.name = "DetalleCita[" + indice + "].Precio";
                txt.setAttribute("data-name", "DetalleCita[{0}].Precio");
                txt.value = precio ;
            }
            else if (i == 4) {
                txt.type = "text";
                txt.name = "DetalleCita[" + indice + "].Duracion";
                txt.setAttribute("data-name", "DetalleCita[{0}].Duracion");
                txt.value = duracion;
            }
        }
        rowl.appendChild(col1);

    }
    (function () {
        var col1 = document.createElement("TD");
        rowl.appendChild(col1);
        var btnEliminar = document.createElement("button");
        col1.appendChild(btnEliminar);
        btnEliminar.innerText = "Eliminar";
        btnEliminar.type = "button";
        btnEliminar.addEventListener("click", () => {
            tbody.removeChild(rowl)
            var trs = tbody.querySelectorAll("tr");
            for (var i = 0; i < trs.length; i++) {
                var item_tr = trs[i];
                var inputs = item_tr.querySelectorAll("input[data-name]");
                for (var j = 0; j < inputs.length; j++) {
                    var input = inputs[j];
                    var dataName = input.getAttribute("data-name");
                    dataName = dataName.replace("{0}", i);
                    input.name = dataName;
                }
            }
        });
    })();



    tbody.appendChild(rowl);
    t1.appendChild(tbody);
}

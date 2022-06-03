////$("#btnCrearNuevos").on('click', function () {
////    $('.modal-body').load("/Sem_Actividad/Crear", function () {
////        $('#modalSemAct').modal({ show: true });

////        ObtenerUUNN();
////        ObtenerTipoActividad();
////        $(document).ready(function () {
            
////            $("#IdUUNN").change(function () {
////                $("#IdSEM").empty();
////                ObtenerSEM_byIdUUNN();
////                $("#IdSEM").querySelectorAll;
////            });

////            $("#IdTipo").change(function () {
////                $("#IdActividad").empty();
////                ObtenerActividadByTipo();
////            });

////            $("#cmbmeses").val([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]);
////            $("#repeticion").change(function () {
////                if (this.value == 1)
////                    $("#cmbmeses").val([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]);
////                else
////                    if (this.value == 2)
////                        $("#cmbmeses").val([2, 4, 6, 8, 10, 12]);
////                    else
////                        if (this.value == 3)
////                            $("#cmbmeses").val([3, 6, 9, 12]);
////                        else
////                            if (this.value == 4)
////                                $("#cmbmeses").val([6, 12]);
////                            else
////                                if (this.value == 5)
////                                    $("#cmbmeses").val([12]);
////            });
////        });

////        $("#frmSEM_ACTIVIDAD").submit(function (event) {
////            event.preventDefault(); //evitar la acción predeterminada para que la pagina no se sobrecargue
////            ////AQUÍ LAS VALIDACIONES EN EL CLIENTE   //
////            var act = $("#IdActividad").val();
////            if (act == "") {
////                alert("Seleccione Actividad.");
////                return;
////            }
////            var sem = $("#IdSEM").val();
////            if (sem == "") {
////                alert("Seleccione una o varias Jefaturas.");
////                return;
////            }
////            var fvenc = $("#digvenc").val();
////            if (fvenc == "") {
////                alert("Ingrese un día del mes para el vencimiento de la Asignación.");
////                return;
////            }
////            var an = $("#anio").val();
////            if (an == "") {
////                alert("Ingrese año.");
////                return;
////            }

////            var post_url = $(this).attr("action"); //action url del formulario
////            var request_method = $(this).attr("method"); //método GET/POST del formulario
////            var form_data = $(this).serializeArray(); //Codificando elementos de formulario para su envío
////            console.log(form_data);
////            LoadingOverlayShow("#frmSEM_ACTIVIDAD"); /* MOSTRAR estado de carga   */
////            $.ajax({
////                url: post_url,
////                type: request_method,
////                data: form_data
////            }).done(function (response) { //
////                console.log(response);
////                //LoadingOverlayHide("#frmSEM_ACTIVIDAD"); /*   OCULTAR estado de carga   */
////                if (response.ok) {
////                    alert("Actividades asignadas exitosamente.");
////                    window.location = response.toRedirect;
////                }
////                else {
////                    alert(response.msg);
////                }
////            }).fail(function (jqXHR, textStatus, errorThrown) {
////                LoadingOverlayHide("#frmSEM_ACTIVIDAD");
////            });
////        });

////    });
////});

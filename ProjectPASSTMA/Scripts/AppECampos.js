function LoadingOverlayShow(id) {
    $(id).LoadingOverlay("show", {
        color: "rgba(255, 255, 255, 0.5)",
        image: "/Content/loading.gif",
        imageResizeFactor: 0.6,
        //imageAnimation: "1.5s fadein",
    });
}

function LoadingOverlayHide(id) {
    $(id).LoadingOverlay("hide");
}



function ObtenerSEDE(myCallback) {
    $.ajax({
        type: "GET",
        url: '/SEDE/GetSEDE',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdSEDE").append('<option value=' + item.IdSEDE + '>' + item.NombreSEDE + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}
function ObtenerUUNN(myCallback) {
    $.ajax({
        type: "GET",
        url: '/UUNN/GetUUNN',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdUUNN").append('<option value=' + item.IdUUNN + '>' + item.NombreUUNN + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error javaScript GetUUNN');
        }
    });
}
function ObtenerSEM(myCallback) {
    $.ajax({
        type: "GET",
        url: '/SEM/GetSEM',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdSEM").append('<option value=' + item.IdSEM + '>' + item.NombreSEM + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}

function ObtenerSEM_byIdUUNN(myCallback) {
    $.ajax({
        type: "GET",
        url: '/SEM/GetSEMByIdUUNN',
        dataType: "json",
        data: { iduunn: $("#IdUUNN").val() },
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdSEM").append('<option value=' + item.IdSEM + ' selected>' + item.NombreSEM + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        
    });
}

function ObtenerSEM_By_User(myCallback) {
    var userName = '@HttpContext.Current.User.Identity.Name'; 
    $.ajax({
        type: "GET",
        url: '/SEM/GetSEMByUser',
        dataType: "json",
        data: { user: $("#username").val() },
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdSEM").append('<option value=' + item.IdSEM + ' selected>' + item.NombreSEM + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },

    });
}

function ObtenerResponsable_sem(myCallback) {
    $.ajax({
        type: "GET",
        url: '/Responsable/GetResponsable',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdResponsable").append('<option value=' + item.IdResponsable + '>' + item.NombreResponsable + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}

function ObtenerFormato(myCallback) {
    $.ajax({
        type: "GET",
        url: '/Formato/GetFormato',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdFormato").append('<option value=' + item.IdFormato + '>' + item.NombreFormato + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}

function ObtenerResponsable(myCallback) {
    $.ajax({
        type: "GET",
        url: '/Responsable/GetResponsable',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdAprobador").append('<option value=' + item.IdResponsable + '>' + item.NombreResponsable + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}

function ObtenerTipoActividad(myCallback) {
    $.ajax({
        type: "GET",
        url: '/TipoActividad/GetTipoActividad',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdTipo").append('<option value=' + item.IdTipo + '>' + item.NombreTipo + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error javaScript GetTipoActividad');
        }
    });
}


function ObtenerEstado(myCallback) {
    $.ajax({
        type: "GET",
        url: '/Estado/GetEstado',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdEstado").append('<option value=' + item.IdEstado + '>' + item.NombreEstado + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}


function ObtenerActividad(myCallback) {
    $.ajax({
        type: "GET",
        url: '/Actividad/GetActividad',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdActividad").append('<option value=' + item.IdActividad + '>' + item.NombreActividad + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }
    });
}

function ObtenerActividadByTipo(myCallback) {
    $.ajax({
        type: "GET",
        url: '/Actividad/GetActividadByTipo',
        dataType: "json",
        data: { idtipo: $("#IdTipo").val() },
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#IdActividad").append('<option value=' + item.IdActividad + '>' + item.NombreActividad + '</option>');
            });

            if (myCallback != undefined)
                return myCallback(result.data);
        },
        //error: function (data) {
        //    alert('error');
        //}
    });
}



//function ValidarFechas(dateIni, dateFin) {
//    var _dateIni = new Date(dateIni);
//    var _dateFin = new Date(dateFin);
//    if (_dateFin < _dateIni)
//        return false;
//    else
//        return true;
//}

//function ListarProyectos(myCallback) {
//    $.ajax({
//        type: "GET",
//        url: '/proyecto/listarproyectos',
//        dataType: "json",
//        success: function (result) {
//            $.each(result.data, function (key, item) {
//                $("#ProyectoId").append('<option value=' + item.ProyectoId + '>' + item.NombreProyecto + '</option>');
//            });

//            if (myCallback != undefined)
//                return myCallback(result.data);
//        },
//        error: function (data) {
//            alert('error');
//        }
//    });
//}

//function ListarEmpleados(myCallback) {
//    $.ajax({
//        type: "GET",
//        url: '/empleado/listarempleados',
//        dataType: "json",
//        success: function (result) {
//            $.each(result.data, function (key, item) {
//                $("#EmpleadoId").append('<option value=' + item.EmpleadoId + '>' + item.Apellidos + ' ' + item.Nombres + '</option>');
//            });

//            if (myCallback != undefined)
//                return myCallback(result.data);
//        },
//        error: function (data) {
//            alert('error');
//        }
//    });
//}
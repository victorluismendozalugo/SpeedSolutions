<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="SpeedSolutions.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/blockUI.css" rel="stylesheet" />
    <link href="Content/toastr.min.css" rel="stylesheet" />

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <link href="js/Components/DataTables/DataTables-1.10.18/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="js/Components/DataTables/Select-1.3.0/css/select.dataTables.min.css" rel="stylesheet" />

</head>
<body class="fixed-nav sticky-footer bg-dark">

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Menu
                </button>
                <div class="dropdown-menu" aria-labelledby="dropdownMenu2">
                    <button class="dropdown-item" type="button" data-toggle="modal" data-target="#modalRegistroUsuarios">Registro usuarios</button>
                    <button class="dropdown-item" type="button" data-toggle="modal" data-target="#modalRegistroProductos" id="btnRegistroProductos">Registro productos</button>
                    <button class="dropdown-item" type="button" data-toggle="modal" data-target="#modalRegistroDispensadores" id="btnRegistroDispensadores">Registro dispensadores</button>
                    <button class="dropdown-item" type="button" data-toggle="modal" data-target="#exampleModal">Cerrar Sesion</button>
                </div>

            </div>
        </ol>
    </nav>

        <!-- Modal registro dispensadores-->
    <div class="modal fade bd-example-modal-lg" id="modalRegistroDispensadores" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registro de dispensadores </h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-3">
                                <label for="exampleInputPassword1">Clave</label>
                                <input type="text" class="form-control" id="" disabled="disabled" value="0" />
                            </div>
                            <div class="col-md-9">
                                <label for="exampleInputPassword1">Estacion</label>
                                <select class="form-control" name="responsable" id="">
                                    <%-- <option value="0"></option>--%>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Nombre producto</label>
                                <input type="text" class="form-control" id="" maxlength="30" />
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Precio</label>
                                <input type="number" class="form-control" id="" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-3">
                                <label for="exampleInputPassword1">Fecha actualizacion</label>
                                <input type="date" class="form-control" id="" disabled="disabled" />
                            </div>
                        </div>
                    </div>

                    <hr />

                    <div class="table-responsive table-sm">
                        <table class="table table-bordered" id="" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>EstacionClave</th>
                                    <th>ProductoClave</th>
                                    <th>Estacion</th>
                                    <th>ProductoNombre</th>
                                    <th>ProductoPrecio</th>
                                    <th>FechaActualizacion</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>EstacionClave</th>
                                    <th>ProductoClave</th>
                                    <th>Estacion</th>
                                    <th>ProductoNombre</th>
                                    <th>ProductoPrecio</th>
                                    <th>FechaActualizacion</th>
                                </tr>
                            </tfoot>
                            <tbody>
                            </tbody>
                        </table>
                    </div>

                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>
                    <button class="btn btn-primary" type="button" id="">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal registro productos-->
    <div class="modal fade bd-example-modal-lg" id="modalRegistroProductos" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registro de productos</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-3">
                                <label for="exampleInputPassword1">Clave</label>
                                <input type="text" class="form-control" id="txtClaveProducto" disabled="disabled" value="0" />
                            </div>
                            <div class="col-md-9">
                                <label for="exampleInputPassword1">Estacion</label>
                                <select class="form-control" name="responsable" id="listadoEstaciones">
                                    <%-- <option value="0"></option>--%>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Nombre producto</label>
                                <input type="text" class="form-control" id="txtProductoNombre" maxlength="30" />
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Precio</label>
                                <input type="number" class="form-control" id="txtProductoPrecio" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-3">
                                <label for="exampleInputPassword1">Fecha actualizacion</label>
                                <input type="date" class="form-control" id="txtFechaActualizacion" disabled="disabled" />
                            </div>
                        </div>
                    </div>

                    <hr />

                    <div class="table-responsive table-sm">
                        <table class="table table-bordered" id="dataTableProductos" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>EstacionClave</th>
                                    <th>ProductoClave</th>
                                    <th>Estacion</th>
                                    <th>ProductoNombre</th>
                                    <th>ProductoPrecio</th>
                                    <th>FechaActualizacion</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>EstacionClave</th>
                                    <th>ProductoClave</th>
                                    <th>Estacion</th>
                                    <th>ProductoNombre</th>
                                    <th>ProductoPrecio</th>
                                    <th>FechaActualizacion</th>
                                </tr>
                            </tfoot>
                            <tbody>
                            </tbody>
                        </table>
                    </div>

                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>
                    <button class="btn btn-primary" type="button" id="btnGuardarProducto">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal registro usuarios-->
    <div class="modal fade bd-example-modal-lg" id="modalRegistroUsuarios" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registro de usuarios</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-3">
                                <label for="exampleInputPassword1">Clave</label>
                                <input type="text" class="form-control" id="txtClaveUsuario" disabled="disabled" value="0" />
                            </div>
                            <div class="col-md-9">
                                <label for="exampleInputPassword1">Login</label>
                                <input type="text" class="form-control" id="txtLogin" disabled="disabled" maxlength="50" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Primer Nombre</label>
                                <input type="text" class="form-control" id="txtPrimerNombre" maxlength="30" />
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Primer Apellido</label>
                                <input type="text" class="form-control" id="txtApellido" maxlength="30" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Contraseña</label>
                                <input type="password" class="form-control" id="txtContraseña" maxlength="50" />
                            </div>
                            <div class="col-md-6">
                                <label for="exampleInputPassword1">Confirme Contraseña</label>
                                <input type="password" class="form-control" id="txtConfirmacion" maxlength="50" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-4">
                                <label for="exampleInputPassword1">Estatus</label>
                                <select class="form-control" name="responsable" id="txtEstatusUsuario">
                                    <option value="1">Activo</option>
                                    <option value="0">Baja</option>
                                </select>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>
                    <button class="btn btn-primary" type="button" id="btnGuardarUsuario">Guardar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Logout Modal-->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Se perderán los cambios no guardados, está seguro de salir?</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">Seleccione "Cerrar sesión" a continuación si está listo para finalizar su sesión actual.</div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>
                    <a class="btn btn-primary" href="Login.aspx">Cerrar sesión</a>
                </div>
            </div>
        </div>
    </div>


    <script src="Scripts/jquery-3.6.0.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>

    <script src="js/Components/DataTables/DataTables-1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="js/Components/DataTables/DataTables-1.10.18/js/dataTables.bootstrap4.min.js"></script>
    <script src="js/Components/DataTables/Select-1.3.0/js/dataTables.select.min.js"></script>

    <script src="js/Components/jquery.blockUI.js"></script>
    <script src="js/Components/toastr.min.js"></script>

    <script src="js/Components/jquery.blockUI.messages.js"></script>
    <script src="js/Components/jquery.AjaxRequest.js"></script>
    <script src="js/Components/jquery.AjaxResult.js"></script>
    <script src="js/Components/FuncionesGenericas.js"></script>

    <script src="js/Components/bootbox.min.js"></script>
    <script src="js/Principal.js"></script>
    <script type="text/javascript">

        var g_config = {
            user: {
                UsuarioClave: '<%=Session["UsuarioClave"]%>',
            }
        };

    </script>
</body>
</html>

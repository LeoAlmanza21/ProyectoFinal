window.onload = () => {
    llenarTabla();
}

let listaProductos = [
    {clave: '1', producto: 'Juguito mango', categoria: 'bebida', existencia: '10', precio: '$12.00', foto: 'IMG', reorden: '10'},
    {clave: '2', producto: 'refresco', categoria: 'bebida', existencia: '10', precio: '$15.00', foto: 'IMG', reorden: '5'},
    {clave: '3', producto: 'papas', categoria: 'comida', existencia: '10', precio: '$18.00', foto: 'IMG', reorden: '5'},
    {clave: '4', producto: 'galletas', categoria: 'comida', existencia: '3', precio: '$17.00', foto: 'IMG', reorden: '7'}
];
let editando = false;

const objProducto = {
    clave: '',
    producto: '',
    categoria: '',
    existencia: '',
    precio: '',
    foto: '',
    reorden: ''
}

function agregarProducto() {
    const clave = document.querySelector('#clave');
    const producto = document.querySelector('#producto');
    const categoria = document.querySelector('#categoria');
    const existencia = document.querySelector('#existencia');
    const precio = document.querySelector('#precio');
    const foto = document.querySelector('#foto');
    const reorden = document.querySelector('#reorden');
    

    objProducto.clave = clave.value;
    objProducto.producto = producto.value;
    objProducto.categoria = categoria.value;
    objProducto.existencia = existencia.value;
    objProducto.precio = precio.value;
    objProducto.foto = foto.value;
    objProducto.reorden = reorden.value;

    if(editando){
        listaProductos.map(producto => {

            if(producto.clave === objProducto.clave) {
                producto.clave = objProducto.clave;
                producto.producto = objProducto.producto;
                producto.categoria = objProducto.categoria;
                producto.existencia = objProducto.existencia;
                producto.precio = objProducto.precio;
                producto.foto = objProducto.foto;
                producto.reorden = objProducto.reorden;
            }
    
        });
        editando = false;
    }else{
        listaProductos.push({ ...objProducto });
    }
    
    objProducto.clave = '';
    objProducto.producto = '';
    objProducto.categoria = '';
    objProducto.existencia = '';
    objProducto.precio = '';
    objProducto.foto = '';
    objProducto.reorden = '';
    
    formulario.querySelector('button[type="reset"]').textContent = 'Agregar';
    formulario.querySelector('input[id="clave"]').readOnly = false;
    
    llenarTabla()
}

function llenarTabla() {
        let lista = listaProductos;
        $(document).ready(() => {
            $('#tblInventario').DataTable({
                destroy: true,
                 data: lista,
                 columns: [
                     {title: 'Clave', data: 'clave'},
                     {title: 'Producto', data: 'producto'},
                     {title: 'Categoria', data: 'categoria'},
                     {title: 'Existencia', data: 'existencia'},
                     {title: 'Precio', data: 'precio'},
                     {title: 'Foto', data: 'foto'},
                     {title: 'Nivel Reorden', data: 'reorden'},
                     {
                        title: 'Actualizar', data: null, render: (data, type) => {
                            return '<button type="button" onclick="actualizar(' +
                                data.clave + ')" class="btn btn-success">Actualizar</button>';
                        }
                    },
                     {
                        title: 'Eliminar', data:null, render: (data, type) => {
                            return '<button type="button" id="save" onclick="eliminar(' +
                                data.clave  +')" class="btn btn-danger">Eliminar</button>'
                                
                        }
                    }
                 ],
                 responsive: {
                    details: {
                        renderer: function (api, rowIdx, columns) {
                            var data = $.map(columns, function (col, i) {
                                return col.hidden ?
                                    '<div class="d-flex flex-wrap me-2"><div class="border border-secondary px-3" style="background-color: #5086c1;">'
                                    + col.title + '</div><div class="border border-secondary px-2 ">' + col.data + '</div></div>' :
                                    '';
                            }).join('');
    
                            return data ?
                                $('<div class="w-100 d-flex flex-wrap"/>').append(data) :
                                false;
                        }
                        
                    }
                },
                columnDefs: [
                    { responsivePriority: 1, targets: [0, 1, 4,7,8] },
                    { responsivePriority: 2, targets: [2,3,5,6] }
                ]
             });
        });
    }
   


function eliminar(clave){
    let indexInArray = listaProductos.findIndex(element => element.clave == clave);
    listaProductos.splice(indexInArray,1);
    let row = indexInArray.parentNode;
    document.getElementById("tblInventario").deleteRow(indexInArray+1);
}


function actualizar(clave) {
    formulario.querySelector('button[type="reset"]').textContent = 'Actualizar';
    formulario.querySelector('input[id="clave"]').readOnly = true;
    listaProductos.forEach(function (value) {
        if (value.clave == clave) {
            document.getElementById('clave').value = value.clave
            document.getElementById('producto').value = value.producto
            document.getElementById('categoria').value = value.categoria
            document.getElementById('existencia').value = value.existencia
            document.getElementById('precio').value = value.precio
            document.getElementById('foto').value = value.foto
            document.getElementById('reorden').value = value.reorden
        }
    })
    editando = true;
}


function actualizar1(id){
    listaProductos.forEach(producto => {
        
        const{clave, productos, categoria, existencia, reorden} = producto;
        if(parseInt(reorden) > parseInt(existencia)){
            console.log("Si es mayor");
        }else{
            console.log("No es mayor");
        }
        
    })
}











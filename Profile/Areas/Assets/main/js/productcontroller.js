var productcontroller = {
    init: function () {
        productcontroller.loadData();
        productcontroller.registerEvent();
        
    },
    registerEvent: function () {
        $('.txtPrice').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                //Value return for :UpdatePrice
                var id =        $(this).data('id');
                var value       =$(this).val();
                var name =$('#Name_' + id).text();
                var metaTitle =$('#MetaTitle_'+id).text();
                var metaKeywords =$('#MetaKeywords_' + id).text();
                var image =$('#Image_'+id).text();
                var quantity = $('#Quantity_+ id').text();
                var status   =$('#Status_'+id).val();
                var promotionPrice =$('#PromotionPrice_' + id).val();
                var createdDate  = $('#CreatedDate_'+id).val();
                var createdBy = $('#CreatedBy_' + id).val();
                var modifiedDate     =  $('#ModifiedDate_'+id).val();
                var topHot = $('#TopHot_' + id).val(); 
                productcontroller.updatePrice(id, value, metaTitle, metaKeywords, image, quantity, status, promotionPrice, createdDate, modifiedDate, topHot);
            }
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            productcontroller.resetForm();
            
        });
        $('#btnsaveValue').off('click').on('click', function () {
            productcontroller.saveProducts();
            $('#modalAddUpdate').modal('hide');
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            productcontroller.loadDetail(id);
        });
        $('.btn-delete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.alert("Do you want to delete record? This cannot be undone.", function (result) {
                productcontroller.deleteProducts(id);
            });
            
        });
    },
    deleteProducts: function (id) {
        $.ajax({
            url: '/Admin/Products/Delete',
            type: 'Delete',
            dataType: 'json',
            data: { id: id },
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Delete success!", function () {
                        productcontroller.loadData();
                    });
                }
                else {
                    bootbox.alert(response.message, function () {
                        productcontroller.loadData();
                    });
                }
            }
        });
    }
    ,
    loadDetail: function (id) {
        $.ajax({
            url: '/Admin/Products/Detail',
            type: 'GET',
            dataType: 'json',
            data: {id: id},
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtModifiedBy').val(data.ModifiedBy);
                    $('#chkStatus').prop('checked', data.Status);
                    //alert("Edit success!");
                    //$('#modalAddUpdate').model('hide');
                }
                else {
                    bootbox.alert({
                        message: response.message,
                        size: 'small'
                    });
                }
            }
        });
    }
    ,
    //Save value when Insert value into DataBase
    saveProducts: function () {
        var id = parseInt($('#hidID').val()); //Check  ID == value 
        var name = $('#txtName').val();
        var metaTitle = $('#txtMetaTitle').val();
       // var modifiedDate = parseDateTime($('#dtModifiedDate').val());
        var modifiedBy = $('#txtModifiedBy').val();
        var status = $('#chkStatus').prop('checked');
        var employee = {ID:id,
            Name: name, MetaTitle: metaTitle,
            Status: status, ModifiedBy: modifiedBy
        };
        $.ajax({
            url: '/Admin/Products/Save',
            type: 'POST',
            data: {
                model: JSON.stringify(employee)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.confirm({
                        message: "Save success!",
                        size: 'large',
                        buttons: {
                            confirm: {
                                label: 'Ok',
                                className: 'btn-success'
                            }
                        },
                        callback: function (result) {
                            console.log('Save success: ' + result);
                        }
                    });
                    $('#modalAddUpdate').modal('hide');
                    productcontroller.loadData();
                } else {
                    bootbox.confirm({
                        message: response.message,
                        size: 'large',
                        buttons: {
                            confirm: {
                                label: 'Ok',
                                className: 'btn-danger'
                            }
                        },
                        callback: function (result) {
                            console.log('Save not success: ' + result);
                        }
                    });
                     
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    ,
    //Update load ajax on form
    updatePrice: function (id, value, metaTitle, metaKeywords, image, quantity, status, promotionPrice, createdDate, modifiedDate, topHot) {
        var data = {
            ID: id, Price: value, MetaTitle: metaTitle, MetaKeywords: metaKeywords, Image: image,
            Quantity: quantity, Status: status, PromotionPrice: promotionPrice, CreatedDate: createdDate, ModifiedDate: modifiedDate, TopHot: topHot
        };
        $.ajax({
            url: '/Admin/Products/Save',
            type: 'POST',
            dataType: 'json',
            data: {model : JSON.stringify(data)},
            success: function (response) {
                if (response.status == true) {
                    alert("Update successed!");
                    productcontroller.loadData();
                }
                else {
                    alert("Update not successed!");
                }
            }
        })
    },
    //Set Value for id from 
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtName').val('');
        $('#txtModifiedBy').val('');
        $('#chkStatus').prop('checked',true);
    }
    ,
    //Get data on view from
    loadData: function () {  
        $.ajax({
            url: '/Admin/Products/GetProducts',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var template = $('#data-template').html();
                   var data = response.data;
                   var html = '';
                   var counter = 1; //Create variable counter
                //Render Html use library Mustache.min.js
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            Counter:counter,
                            ID: item.ID,
                            Name: item.Name,
                            MetaTitle: item.MetaTitle,
                            MetaKeywords: item.MetaKeywords,
                            Price: item.Price,
                            Image: item.Image,
                            Quantity: item.Quantity,
                            Status: item.Status == true ? "<span class=\"label label-success\">Active</span>" : "<span class=\"label label-danger\">Locked</span>",
                            PromotionPrice: item.PromotionPrice,
                            CreatedDate: item.CreatedDate,
                            CreatedBy: item.CreatedBy,
                            ModifiedDate: item.ModifiedDate,
                            TopHot: item.TopHot
                        });
                        counter++; //counter++
                    });
                    $('#products').html(html);
                    productcontroller.registerEvent();
            },
            error: function (response) {
                console.log(response);
            }
        })
    }
}
productcontroller.init();

class ShoppingCartJS {

    clickIncrement(btn) {
        let data = this.getData(btn);
        data.Amount++;
        this.postAmount(data);
    }

    clickDecrement(btn) {
        let data = this.getData(btn);
        data.Amount--;
        if (data.Amount < 0) data.Amount = 0;
        this.postAmount(data);
    }

    updateAmount(input) {
        let data = this.getData(input);
        if (data.Amount < 0) data.Amount = 0;
        this.postAmount(data);
    }

    getData(element) {
        /* jQuery */
        var itemLine = $(element).parents("[item-id]");
        var itemId = $(itemLine).attr("item-id");
        var newAmount = $(itemLine).find("input").val();

        var data = {
            Id: itemId,
            Amount: newAmount
        };
        return data;
    }

    postAmount(data) {
        /* Asynchronous JavaScript Xml (Nowadays we use another pattern that is Json)*/
        $.ajax({
            url: "/Order/UpdateAmount",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data)
        }).done(function (response) {
            let orderItem = response.orderItem;
            let itemLine = $("[item-id=" + orderItem.id + "]");
            itemLine.find("input").val(orderItem.amount);
            itemLine.find("[subtotal]").html((orderItem.subtotal).twoDigits());
            let shoppingCartViewModel = response.shoppingCartViewModel;            
            $("[total]").html((shoppingCartViewModel.total).twoDigits());
            $("[number-items]").html("Total " + shoppingCartViewModel.items.length + " items");
            if (orderItem.amount == 0)
                itemLine.remove();
        });
    }
    
}

var shoppingCartJS = new ShoppingCartJS();

Number.prototype.twoDigits = function () {
    return this.toFixed(2).replace('.', ',');
}

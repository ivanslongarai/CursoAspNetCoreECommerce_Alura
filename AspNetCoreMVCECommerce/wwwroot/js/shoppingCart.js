
class ShoppingCartJS {

    clickIncrement(button) {
        let data = this.getData(button);
        data.Amount++;
        this.postAmount(data);
    }

    clickDecrement(button) {
        let data = this.getData(button);
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

        let token = $("[name=__RequestVerificationToken]").val();
        let headers = {};
        headers["RequestVerificationToken"] = token;

        $.ajax({
            url: "/Order/UpdateAmount",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            headers: headers
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

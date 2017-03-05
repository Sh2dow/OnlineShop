var vm = {
    topCategories: [
        { name: 'Antiques', id: 20081},
        { name: 'Art', id: 550},
        { name: 'Baby', id: 2984 },
        { name: 'Books, Comics & Magazines', id: 267 },
        { name: 'Business, Office & Industrial', id: 12576 },
        { name: 'Cameras & Photography', id: 625 },
        { name: 'Cars, Motorcycles & Vehicles', id: 9800 },
        { name: 'Clothes, Shoes & Accessories', id: 11450 },
        { name: 'Coins', id: 11116 },
        { name: 'Collectables', id: 1 },
        { name: 'Computers/Tablets & Networking', id: 58058 },
        { name: 'Crafts', id: 14339 },
        { name: 'Dolls & Bears', id: 237 },
        { name: 'DVDs, Films & TV', id: 11232 },
        { name: 'Events Tickets', id: 1305 },
        { name: 'Garden & Patio', id: 159912 },
        { name: 'Health & Beauty', id: 26395 },
        { name: 'Holidays & Travel', id: 3252 },
        { name: 'Home, Furniture & DIY', id: 11700 },
        { name: 'Jewellery & Watches', id: 281 },
        { name: 'Mobile Phones & Communication', id: 15032 },
        { name: 'Music', id: 11233 },
        { name: 'Musical Instruments', id: 619 },
        { name: 'Pet Supplies', id: 1281 },
        { name: 'Pottery, Porcelain & Glass', id: 870 },
        { name: 'Property', id: 10542 },
        { name: 'Sound & Vision', id: 293 },
        { name: 'Sporting Goods', id: 888 },
        { name: 'Sports Memorabilia', id: 64482 },
        { name: 'Stamps', id: 260 },
        { name: 'Toys & Games', id: 220 },
        { name: 'Vehicle Parts & Accessories', id: 131090 },
        { name: 'Video Games & Consoles', id: 1249 },
        { name: 'Wholesale & Job Lots', id: 40005 },
        { name: 'Everything Else', id: 99 },
    ],
    selectedCategory: ko.observable()
};

var dataToPost = {};
//bypass the cross-origin resource sharing problem by using YQL to proxy the request through Yahoos servers
self.proceed = function () {
    var yql_url = 'https://query.yahooapis.com/v1/public/yql';
    var url = 'http://open.api.ebay.com/shopping?version=957&appid=IgLov-OnlineSh-PRD-32466ad44-6e57bd31&callname=FindPopularItems&categoryId=' + vm.selectedCategory().id + '&ResponseEncodingType=JSON';
    console.log(url)
    $.ajax({
        'url': yql_url,
        'type': "POST",
        'data': {
            'q': 'SELECT * FROM json WHERE url="' + url + '"',
            'format': 'json',
            'jsonCompat': 'new',
        },
        'dataType': 'json',
        'success': function (response) {
            dataToPost = ko.mapping.fromJS(response, {}, self);
            var model = ko.mapping.toJSON(dataToPost);
            //console.log(model);
            $.ajax({
                url: "/Home/SaveProducts",
                type: "POST",
                data: model,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (message) {
                    ko.mapping.fromJS(data.viewModel, {}, self);
                    if (message.Status === "success") {
                        toastr.success(message.Content);
                    } else if (message.Status === "error") {
                        toastr.error(message.Content);
                    }
                }
            });
        },
    });
};

ko.applyBindings(vm);

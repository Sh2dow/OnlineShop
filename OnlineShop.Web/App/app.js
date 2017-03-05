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

self.process = function (param) {
    console.log('/Home/SaveProducts?param=' + param);
    window.location = '/Home/SaveProducts?param=' + param;
};

ko.applyBindings(vm);

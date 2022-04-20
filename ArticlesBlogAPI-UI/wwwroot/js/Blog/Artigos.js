class Artigos {
    constructor() {
        this.apiArticles = "/Articles/All";
        this.apiArticleById = "/Articles/"; //+id_article
        this.apiArticleSearch = "/Articles/Search?searchedText=";//+texto busca
        this.loadFeedArticle();
        this.setBtnEvents();
    }

    loadFeedArticle() {
        $.get(this.apiArticles, function (data, status) {

            data.forEach(article => {
                $("#feed-articles").append('<div class="card" style="max-width: 50rem;"><img src="' + article.urlBanner + '" class="card-img-top" alt="..."><div class="card-body"><h5 class="card-title">' + article.title + '</h5><p class="card-text">' + article.summary + '</p><a href="#" class="btn btn-primary">Ver artigo</a></div></div>');
            });
        });
    }

    setBtnEvents() {
        var MyClass = this;
        $("#frm-procura-artigos").on('submit', function () {
            var textoBusca = $("#campo-busca-artigo").val();
            if (textoBusca.trim() != '') {

                $.get(MyClass.apiArticleSearch + textoBusca, function (data, status) {
                    
                    $("#feed-articles").html('');

                    if (data.success == false) {
                        $("#aviso-nao-encontrado").css("display", "flex");

                    } else {
                        $("#aviso-nao-encontrado").css("display", "none");
                        data.forEach(article => {
                            $("#feed-articles").append('<div class="card" style="max-width: 50rem;"><img src="' + article.urlBanner + '" class="card-img-top" alt="..."><div class="card-body"><h5 class="card-title">' + article.title + '</h5><p class="card-text">' + article.summary + '</p><a href="#" class="btn btn-primary">Ver artigo</a></div></div>');
                        });
                    }
                });
            }else{
                $("#feed-articles").html('');
                $("#aviso-nao-encontrado").css("display", "none");
                MyClass.loadFeedArticle();
            }
        });
    }
}

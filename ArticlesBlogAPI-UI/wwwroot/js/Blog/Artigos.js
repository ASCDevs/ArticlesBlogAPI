class Artigos {
    constructor() {
        this.apiArticles = "/Articles/All";
        this.apiArticleById = "/Articles/"; //+id_article
        this.apiArticleSearch = "/Articles/Search?searchedText=";//+texto busca
        this.apiAuthorById = "/Articles/Authors/"; //+id
        this.artigosList;
        this.setFunctions();
        this.loadFeedArticle();
        this.setBtnEvents();
    }

    loadFeedArticle() {
        var MyClass = this;
        $.get(this.apiArticles, function (data, status) {
            MyClass.artigosList = data;
            data.forEach(article => {
                let index = MyClass.returnArticleIndex(MyClass.artigosList,article.idArticle);
                $("#feed-articles").append('<div class="card" style="max-width: 50rem;"><img src="' + article.urlBanner + '" class="card-img-top" alt="..."><div class="card-body"><h5 class="card-title">' + article.title + '</h5><p class="card-text">' + article.summary + '</p><button type="button" class="btn btn-primary btn-ler-artigo" data-index='+index+'>Ler artigo</button></div></div>');
            });
            MyClass.setBtnsLerArtigos();
        });
    }

    setFunctions(){
        var MyClass = this;

        this.setBtnsLerArtigos = function(){
            $(".btn-ler-artigo").click(function(){
                let index = $(this).data("index");
                let artigo = MyClass.artigosList[index];

                $.get(MyClass.apiAuthorById+artigo.idAuthor, function (data, status) {
                    $("#autor-artigo").text(data.name);
                    $("#foto-autor-artigo").attr('src',data.urlPhoto);
                });


                $("#titulo-artigo").text(artigo.title);
                $("#banner-artigo").attr("src",artigo.urlBanner)
                $("#texto-artigo").text(artigo.textArticle)
                $("#data-artigo").text(artigo.datePublic);
                
                $("#modal-artigo").modal("show");
            });
        }

        this.returnArticleIndex = function(Artigos, id_article){
            let index = Artigos.findIndex(function(obj){
                return obj.idArticle === id_article
            });
            return index;
        }
    }

    setBtnEvents() {
        var MyClass = this;
        
        $("#frm-procura-artigos").on('submit', function () {
            var textoBusca = $("#campo-busca-artigo").val();
            if (textoBusca.trim() != '') {

                $.get(MyClass.apiArticleSearch + textoBusca, function (data, status) {
                    
                    $("#feed-articles").html('');

                    if (data.success == false) {
                        MyClass.artigosList = null;
                        $("#aviso-nao-encontrado").css("display", "flex");

                    } else {
                        $("#aviso-nao-encontrado").css("display", "none");
                        MyClass.artigosList = data;

                        data.forEach(article => {
                            let index = MyClass.returnArticleIndex(MyClass.artigosList,article.idArticle);
                            $("#feed-articles").append('<div class="card" style="max-width: 50rem;"><img src="' + article.urlBanner + '" class="card-img-top" alt="..."><div class="card-body"><h5 class="card-title">' + article.title + '</h5><p class="card-text">' + article.summary + '</p><button class="btn btn-primary btn-ler-artigo" data-index='+index+'>Ler Artigo</button></div></div>');
                        });
                    }
                    MyClass.setBtnsLerArtigos();
                });
            }else{
                $("#feed-articles").html('');
                $("#aviso-nao-encontrado").css("display", "none");
                MyClass.loadFeedArticle();
            }
        });
        
    }
}

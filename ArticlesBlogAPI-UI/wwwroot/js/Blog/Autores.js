class Autores {
    constructor() {
        this.apiAuthors = "/Articles/Authors";
        this.apiAuthorById = "/Articles/Authors/"; //+id_author
        this.apiAuthorByName = "/Articles/Authors/Search?authorName="; //+nome do autor
        this.apiArticleById = "/Articles/"; //id do artigo
        this.apiAuthorArticles = "/Articles/AuthorWritten/"; //+id do autor
        this.autoresList;
        this.setFunctions();
        this.loadFeedAuthors();
        this.setBtnEvents();
    }

    setFunctions(){
        var MyClass = this;

        this.setBtnsLerArtigos = function(){
            $(".btn-ler-artigo").click(function(){
                let idArtigo = $(this).data("idarticle");
                let indexAutor = $(this).data("indexautor");
                let autor = MyClass.autoresList[indexAutor];

                $.get(MyClass.apiArticleById+idArtigo, function (data, status) {
                    $("#titulo-artigo").text(data.title);
                    $("#banner-artigo").attr("src",data.urlBanner)
                    $("#texto-artigo").text(data.textArticle)
                    $("#data-artigo").text(data.datePublic);
                });

                $("#autor-artigo").text(autor.name);
                $("#foto-autor-artigo").attr('src',autor.urlPhoto);
                
                
                $("#modal-artigo").modal("show");
            });
        }

        this.setBtnsMostrarArtigos = function(){
            $(".btn-mostrar-artigos").click(function(){
                $("#feed-articles").html('');
                let indexAutor = $(this).data("index");
                let autor = MyClass.autoresList[indexAutor];

                $.get(MyClass.apiAuthorArticles+autor.idAuthor, function (data, status) {
                    
                    data.forEach(article => {
                        $("#feed-articles").append('<div class="card" style="max-width: 50rem;"><div class="card-body"><h5 class="card-title">' + article.title + '</h5><p class="card-text">' + article.summary + '</p><p style="color: #ADB5BD;"><i class="fa-solid fa-calendar-days"></i> Publicação: '+article.datePublic+'</span></p><button class="btn btn-primary btn-ler-artigo" data-idarticle='+article.idArticle+' data-indexautor='+indexAutor+'>Ler Artigo</button></div></div>');
                    })
                    MyClass.setBtnsLerArtigos();
                });

                $("#escrito-por").text(autor.name);

                
                $("#modal-autor-artigos").modal("show");
            });
        }

        this.returnAuthorIndex = function(Autores, id_author){
            let index = Autores.findIndex(function(obj){
                return obj.idAuthor === id_author
            });
            return index;
        }
    }

    loadFeedAuthors() {
        var MyClass = this;
        $.get(this.apiAuthors, function (data, status) {
            MyClass.autoresList = data;
            data.forEach(author => {
                let index = MyClass.returnAuthorIndex(MyClass.autoresList, author.idAuthor)
                $("#feed-authors").append('<div class="card d-flex flex-row" style="max-width: 50rem;"><div class="d-flex justify-content-center align-items-center"><img src="' + author.urlPhoto + '" style="max-width:8rem; border-radius: 50%" alt="..."></div><div class="card-body"><h5 class="card-title">' + author.name + '</h5><p class="card-text">' + author.biography + '</p><p class="card-text"><small>' + author.institute + '</small></p><button class="btn btn-primary btn-mostrar-artigos" data-index='+index+'>Mostrar artigos escritos</button></div></div>');
            });

            MyClass.setBtnsMostrarArtigos();
        });
    }

    setBtnEvents() {
        var MyClass = this;
        $("#frm-procura-autores").on('submit', function () {
            var textoBusca = $("#campo-busca-autores").val();
            if (textoBusca.trim() != '') {

                $.get(MyClass.apiAuthorByName + textoBusca, function (data, status) {
                    
                    $("#feed-authors").html('');

                    if (data.success == false) {
                        MyClass.autoresList = null
                        $("#aviso-nao-encontrado").css("display", "flex");

                    } else {
                        MyClass.autoresList = data;
                        $("#aviso-nao-encontrado").css("display", "none");
                        data.forEach(author => {
                            let index = MyClass.returnAuthorIndex(MyClass.autoresList, author.idAuthor)
                            $("#feed-authors").append('<div class="card d-flex flex-row" style="max-width: 50rem;"><div class="d-flex justify-content-center align-items-center"><img src="' + author.urlPhoto + '" style="max-width:8rem; border-radius: 50%" alt="..."></div><div class="card-body"><h5 class="card-title">' + author.name + '</h5><p class="card-text">' + author.biography + '</p><p class="card-text"><small>' + author.institute + '</small></p><button class="btn btn-primary btn-mostrar-artigos" data-index='+index+'>Mostrar artigos escritos</button></div></div>');
                        });
                    }

                    MyClass.setBtnsMostrarArtigos();
                });
            }else{
                $("#aviso-nao-encontrado").css("display", "none");
                $("#feed-authors").html('');
                MyClass.loadFeedAuthors();
            }
        });
    }
}
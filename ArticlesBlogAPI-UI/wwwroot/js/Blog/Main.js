//Global variables
var artigos;
var autores;

$(document).ready(function(){
    artigos = new Artigos();
    autores = new Autores();
});


class Artigos{
    constructor(){
        this.apiArticles = "/Articles/All";
        this.apiArticleById = "/Articles/"; //+id_article
        this.apiArticleSearch = "/Articles/Search?searchedText=";//+texto busca
        this.loadFeedArticle();
        this.setBtnEvents();
    }

    loadFeedArticle(){
        $.get(this.apiArticles, function(data,status){
            data.forEach(article => {
                $("#feed-articles").append('<div class="card" style="max-width: 50rem;"><img src="..." class="card-img-top" alt="..."><div class="card-body"><h5 class="card-title">'+article.title+'</h5><p class="card-text">'+article.textArticle+'</p><a href="#" class="btn btn-primary">Ver artigo</a></div></div>');
            });
        });
    }

    setBtnEvents(){

    }
}

class Autores{
    constructor(){
        this.apiAuthors = "/Articles/Authors";
        this.apiAuthorById = "/Articles/Authors/"; //+id_author
        this.apiAuthorByName = "/Articles/Authors/Search?authorName="; //+nome do autor
        this.loadFeedAuthors();
    }

    loadFeedAuthors(){
        $.get(this.apiAuthors, function(data,status){
            data.forEach(author => {
                $("#feed-authors").append('<div class="card d-flex flex-row" style="max-width: 50rem;"><div class="d-flex justify-content-center align-items-center"><img src="'+author.urlPhoto+'" style="max-width:8rem; border-radius: 50%" alt="..."></div><div class="card-body"><h5 class="card-title">'+author.name+'</h5><p class="card-text">'+author.biography+'</p><p class="card-text"><small>'+author.institute+'</small></p><a href="#" class="btn btn-primary">Artigos escritos</a></div></div>');
            });
        });
    }

    setBtnEvents(){

    }
}
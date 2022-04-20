class Autores {
    constructor() {
        this.apiAuthors = "/Articles/Authors";
        this.apiAuthorById = "/Articles/Authors/"; //+id_author
        this.apiAuthorByName = "/Articles/Authors/Search?authorName="; //+nome do autor
        this.loadFeedAuthors();
        this.setBtnEvents();
    }

    loadFeedAuthors() {

        $.get(this.apiAuthors, function (data, status) {
            data.forEach(author => {
                $("#feed-authors").append('<div class="card d-flex flex-row" style="max-width: 50rem;"><div class="d-flex justify-content-center align-items-center"><img src="' + author.urlPhoto + '" style="max-width:8rem; border-radius: 50%" alt="..."></div><div class="card-body"><h5 class="card-title">' + author.name + '</h5><p class="card-text">' + author.biography + '</p><p class="card-text"><small>' + author.institute + '</small></p><a href="#" class="btn btn-primary">Artigos escritos</a></div></div>');
            });
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
                        $("#aviso-nao-encontrado").css("display", "flex");

                    } else {
                        $("#aviso-nao-encontrado").css("display", "none");
                        data.forEach(author => {
                            $("#feed-authors").append('<div class="card d-flex flex-row" style="max-width: 50rem;"><div class="d-flex justify-content-center align-items-center"><img src="' + author.urlPhoto + '" style="max-width:8rem; border-radius: 50%" alt="..."></div><div class="card-body"><h5 class="card-title">' + author.name + '</h5><p class="card-text">' + author.biography + '</p><p class="card-text"><small>' + author.institute + '</small></p><a href="#" class="btn btn-primary">Artigos escritos</a></div></div>');
                        });
                    }
                });
            }else{
                $("#aviso-nao-encontrado").css("display", "none");
                $("#feed-authors").html('');
                MyClass.loadFeedAuthors();
            }
        });
    }
}
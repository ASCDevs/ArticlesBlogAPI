using ArticlesBlogAPI_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesBlogAPI_UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {

        private List<Article> articles;
        private List<Author> authors;

        public ArticlesController() 
        {
            this.MockData();
        }

        [HttpGet]
        [Route("All")]
        public IEnumerable<Article> GetAllArticles()
        {
            return articles.ToList();
        }

        [HttpGet]
        [Route("Authors")]
        public IEnumerable<Author> GetAllAuthors()
        {
            return this.authors.ToList();
        }

        [HttpGet]
        [Route("{id_article}")]
        public IActionResult GetArticleById(int id_article)
        {
            Article? article = articles.FirstOrDefault(x => x.idArticle == id_article);
            if (article == null) return NotFound(new {success = false, message = "Artigo não encontrado"});

            return Ok(article);
        }

        [HttpGet]
        [Route("Authors/{id_author}")]
        public IActionResult GetAuthorById(int id_author)
        {
            Author? author = authors.FirstOrDefault(x => x.idAuthor == id_author);
            if (author == null) return NotFound(new { success = false, message = "Autor não foi encontrado" });

            return Ok(author);
        }

        [HttpGet]
        [Route("Authors/Search")]
        public IActionResult SearchAuthorsByName(string authorName)
        {
            List<Author> authorsFound = authors.AsQueryable().Where(x => x.name.Trim().ToLower().Contains(authorName.ToLower().Trim())).ToList();
            if (authorsFound == null || authorsFound.Count() == 0) return NotFound(new { success = false, message = "Nenhum autor encontrado com o nome indicado" });

            return Ok(authorsFound);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult SearchArticles(string searchedText)
        {
            List<string> words = searchedText.Split(" ").ToList();
            List<Article> articlesFound = articles.AsQueryable().Where(x => x.textArticle.Trim().ToLower().Contains(searchedText.ToLower().Trim()) || words.Any(w => x.textArticle.Trim().ToLower().Contains(w)) || words.Any(w => x.tags.Any(y => y.Trim().ToLower().Contains(w)))).ToList();
            if (articlesFound == null || articlesFound.Count() == 0) return NotFound(new { success = false, message = "Nenhum artigo encontrado com o termo de busca" });

            return Ok(articlesFound);
        }



        private void MockData()
        {
            this.articles = new List<Article>();
            articles.Add(new Article
            {
                idArticle = 1,
                datePublic = DateTime.Now.ToString("dd/MM/yyyy"),
                textArticle = @"Atualmente os consumidores estão mais exigentes e sensíveis quanto às questões ambientais.  Segundo pesquisa realizada pela Akatu em 2018, constatou-se que 40% dos consumidores mudam sua intenção de compra para produtos que visam proteger o meio ambiente.
                                Logo, empresas que não levam em conta a reciclagem, que não avaliam o uso de poluentes e ignoram medidas que fazem bem para a saúde do planeta, provocam olhares de reprovação de um nicho crescente de consumidores e perdem a chance de se posicionar com uma vantagem competitiva.
                                Hoje em dia, diante da velocidade da informação em vídeos, redes sociais e blogs, fica difícil reparar os danos que uma má reputação pode provocar na marca de uma empresa. Por causa disso, priorizar a sustentabilidade deve ser parte da cultura da organização.
                                A Internet das Coisas, por exemplo, permite que alguns processos de trabalho em um negócio atendam aos requisitos sustentáveis, causando o mínimo de danos ao meio ambiente de forma otimizada.
                                As vantagens que um negócio tem ao adotar uma postura sustentável são várias, não importa se o empreendimento é de grande, pequeno ou médio porte.",
                idAuthor = 1,
                title = "IOT e Sustentabilidade",
                tags = new List<string> { "IOT", "Sustentabilidade", "Programação" }
            });
            
            articles.Add(new Article
            {
                idArticle = 2,
                datePublic = DateTime.Now.ToString("dd/MM/yyyy"),
                textArticle = @"O mercado de trabalho está carente de trabalhadores na área de tecnologia de informação (TI) que sabem aliar estratégias a habilidades técnicas. Isso quer dizer que as empresas estão à procura de pessoas que sejam capazes de colocar a mão na massa e, também, entender as necessidades do cliente.
                                Neste post, você vai saber um pouco mais sobre as profissões que estão em alta nessa área e qual é a formação necessária para conquistar as melhores vagas.
                                Segundo o portal Computerworld, 39% das empresas acreditam que vão precisar contratar gestores de projetos ainda em 2016. Quem atua no mercado corporativo sabe que, cada vez mais, os projetos são integrados. Isto é, existem profissionais de diversas especialidades trabalhando em conjunto.",
                idAuthor = 2,
                title = "Formação de profissionais da área de TI",
                tags = new List<string> { "Mercado", "TI", "Estudo" }
            });

            articles.Add(new Article
            {
                idArticle = 3,
                datePublic = DateTime.Now.ToString("dd/MM/yyyy"),
                textArticle = @"Um dos projetos mais cobiçados pelos amantes da tecnologia é a automação residencial Arduino, onde você pode, por meio de aplicativos, comandar a casa como um todo, permitindo abrir e fechar cortinas e janelas motorizadas, ligar e desligar televisores em horários pré-definidos, comandar ventiladores e tudo mais que você pensar, isso diretamente em seu telefone, tablet ou computador.
                                Grandes coisas em automação podem serem feitas adicionando ao Arduino um Ethernet Shield, permitindo a você transformar o pequeno microcontrolador em um dispositivo conectado à Internet, capaz de mudar o estado de luzes, TVs, máquinas de café – praticamente qualquer coisa que você pode pensar – ligado e desligado, utilizando uma interface baseada em browser ou um temporizador.",
                idAuthor = 3,
                title = "Cadeiras automatizadas com arduino",
                tags = new List<string> { "Arduino", "Cadeiras", "Programação" }
            });

            this.authors = new List<Author>();
            authors.Add(new Author 
            { 
                idAuthor = 1,
                name = "Heloísa Silva",
                urlPhoto = "https://github.com/HeloisaSil.png"
            });
            authors.Add(new Author
            {
                idAuthor = 2, 
                name = "Rafaela Santos",
                urlPhoto = "https://github.com/rafass04.png"
            });
            authors.Add(new Author
            {
                idAuthor = 3,
                name = "Alexandre Santos",
                urlPhoto = "https://github.com/ascdevs.png"
            });
        }
    }
}

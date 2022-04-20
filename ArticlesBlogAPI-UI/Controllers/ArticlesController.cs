using ArticlesBlogAPI_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesBlogAPI_UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private List<Article>? articles;
        private List<Author>? authors;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(ILogger<ArticlesController> logger)
        {
            this.MockData();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("All")]
        public IEnumerable<Article> GetAllArticles()
        {
            return articles!.ToList();
        }

        [HttpGet]
        [Route("Authors")]
        public IEnumerable<Author> GetAllAuthors()
        {
            return this.authors!.ToList();
        }

        [HttpGet]
        [Route("{id_article}")]
        public IActionResult GetArticleById(int id_article)
        {
            _logger.LogInformation("Searching articles by id");
            Article? article = articles!.FirstOrDefault(x => x.IdArticle == id_article);
            if (article == null) return Ok(new { success = false, message = "Artigo não encontrado." });

            return Ok(article);
        }

        [HttpGet]
        [Route("Authors/{id_author}")]
        public IActionResult GetAuthorById(int id_author)
        {
    
            _logger.LogInformation("Searching articles by authors");
            Author? author = authors!.FirstOrDefault(x => x.IdAuthor == id_author);
            if (author == null) return Ok(new { success = false, message = "Autor não encontrado." });

            return Ok(author);

        }

        [HttpGet]
        [Route("Authors/Search")]
        public IActionResult SearchAuthorsByName(string authorName)
        {
            _logger.LogInformation("Searching articles by authors");
            List<Author> authorsFound = authors!.AsQueryable().Where(x => x.Name!.Trim().ToLower().Contains(authorName.ToLower().Trim())).ToList();
            if (authorsFound == null || authorsFound.Count == 0) return Ok(new { success = false, message = "Nenhum autor encontrado com esse nome." });

            return Ok(authorsFound);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult SearchArticles(string searchedText)
        {
            _logger.LogInformation("Searching articles by term");
            List<string> words = searchedText.Split(" ").ToList();
            List<Article> articlesFound = articles!.AsQueryable().Where(x => x.TextArticle!.Trim().ToLower().Contains(searchedText.ToLower().Trim()) || words.Any(w => x.TextArticle.Trim().ToLower().Contains(w)) || words.Any(w => x.Tags!.Any(y => y.Trim().ToLower().Contains(w)))).ToList();
            if (articlesFound == null || articlesFound.Count() == 0) return Ok(new { success = false, message = "Nenhum artigo encontrado com o termo dado." });

            return Ok(articlesFound);
        }

        private void MockData()
        {
            this.articles = new List<Article>();
            articles.Add(new Article
            {
                IdArticle = 1,
                DatePublic = DateTime.Now.ToString("dd/MM/yyyy"),
                TextArticle = @"Atualmente os consumidores estão mais exigentes e sensíveis quanto às questões ambientais.  Segundo pesquisa realizada pela Akatu em 2018, constatou-se que 40% dos consumidores mudam sua intenção de compra para produtos que visam proteger o meio ambiente.
                                Logo, empresas que não levam em conta a reciclagem, que não avaliam o uso de poluentes e ignoram medidas que fazem bem para a saúde do planeta, provocam olhares de reprovação de um nicho crescente de consumidores e perdem a chance de se posicionar com uma vantagem competitiva.
                                Hoje em dia, diante da velocidade da informação em vídeos, redes sociais e blogs, fica difícil reparar os danos que uma má reputação pode provocar na marca de uma empresa. Por causa disso, priorizar a sustentabilidade deve ser parte da cultura da organização.
                                A Internet das Coisas, por exemplo, permite que alguns processos de trabalho em um negócio atendam aos requisitos sustentáveis, causando o mínimo de danos ao meio ambiente de forma otimizada.
                                As vantagens que um negócio tem ao adotar uma postura sustentável são várias, não importa se o empreendimento é de grande, pequeno ou médio porte.",
                IdAuthor = 1,
                Title = "IOT e Sustentabilidade",
                Tags = new List<string> { "IOT", "Sustentabilidade", "Programação" },
                Summary = @"Artigo que traz uma relação entre Internet das Coisas e o tema de sustentabilidade, mostrando a aplicabilidade.",
                UrlBanner = "https://source.unsplash.com/4NhqyQeErP8/800x300"
            });

            articles.Add(new Article
            {
                IdArticle = 2,
                DatePublic = DateTime.Now.ToString("dd/MM/yyyy"),
                TextArticle = @"O mercado de trabalho está carente de trabalhadores na área de tecnologia de informação (TI) que sabem aliar estratégias a habilidades técnicas. Isso quer dizer que as empresas estão à procura de pessoas que sejam capazes de colocar a mão na massa e, também, entender as necessidades do cliente.
                                Neste post, você vai saber um pouco mais sobre as profissões que estão em alta nessa área e qual é a formação necessária para conquistar as melhores vagas.
                                Segundo o portal Computerworld, 39% das empresas acreditam que vão precisar contratar gestores de projetos ainda em 2016. Quem atua no mercado corporativo sabe que, cada vez mais, os projetos são integrados. Isto é, existem profissionais de diversas especialidades trabalhando em conjunto.",
                IdAuthor = 2,
                Title = "Formação de profissionais da área de TI",
                Tags = new List<string> { "Mercado", "TI", "Estudo" },
                Summary = @"Nos dias de hoje o mercado de TI está super aquiecido porém é importante estar atento aos tipos de formações da área e conseguir focar os estudos.",
                UrlBanner = "https://source.unsplash.com/Kz8nHVg_tGI/800x300"
            });

            articles.Add(new Article
            {
                IdArticle = 3,
                DatePublic = DateTime.Now.ToString("dd/MM/yyyy"),
                TextArticle = @"Um dos projetos mais cobiçados pelos amantes da tecnologia é a automação residencial Arduino, onde você pode, por meio de aplicativos, comandar a casa como um todo, permitindo abrir e fechar cortinas e janelas motorizadas, ligar e desligar televisores em horários pré-definidos, comandar ventiladores e tudo mais que você pensar, isso diretamente em seu telefone, tablet ou computador.
                                Grandes coisas em automação podem serem feitas adicionando ao Arduino um Ethernet Shield, permitindo a você transformar o pequeno microcontrolador em um dispositivo conectado à Internet, capaz de mudar o estado de luzes, TVs, máquinas de café – praticamente qualquer coisa que você pode pensar – ligado e desligado, utilizando uma interface baseada em browser ou um temporizador.",
                IdAuthor = 3,
                Title = "Cadeiras automatizadas com arduino",
                Tags = new List<string> { "Arduino", "Cadeiras", "Programação" },
                Summary = @"A automatização hoje em dia é possível através de várias tecnologias, entre elas o arduino, no artigo abordaremos como automatizar a regulagem de uma cadeira de escritório.",
                UrlBanner = "https://source.unsplash.com/zP7X_B86xOg/800x300"
            });

            this.authors = new List<Author>();
            authors.Add(new Author
            {
                IdAuthor = 1,
                Name = "Heloísa Silva",
                UrlPhoto = "https://github.com/HeloisaSil.png",
                Institute = "UNIP - Universidade Paulista",
                Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
            });
            authors.Add(new Author
            {
                IdAuthor = 2,
                Name = "Rafaela Santos",
                UrlPhoto = "https://github.com/rafass04.png",
                Institute = "UNIP - Universidade Paulista",
                Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
            });
            authors.Add(new Author
            {
                IdAuthor = 3,
                Name = "Alexandre Santos",
                UrlPhoto = "https://github.com/ascdevs.png",
                Institute = "UNIP - Universidade Paulista",
                Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
            });
        }
    }
}
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
            List<string> words = searchedText.ToLower().Split(" ").ToList();
            List<Article> articlesFound = articles!.AsQueryable().Where(x => x.TextArticle!.Trim().ToLower().Contains(searchedText.ToLower().Trim()) || words.Any(w => x.TextArticle.Trim().ToLower().Contains(w)) || words.Any(w => x.Tags!.Any(y => y.Trim().ToLower().Contains(w)))).ToList();
            if (articlesFound == null || articlesFound.Count() == 0) return Ok(new { success = false, message = "Nenhum artigo encontrado com o termo dado." });

            return Ok(articlesFound);
        }

        [HttpGet]
        [Route("AuthorWritten/{id_author}")]
        public IActionResult SearchArticlesByAuthor(int id_author){
            _logger.LogInformation("Searching articles by author id");
            List<Article> articlesFound = articles!.AsQueryable().Where(x => x.IdAuthor == id_author).ToList();
            if (articlesFound == null || articlesFound.Count() == 0) return Ok(new { success = false, message = "Nenhum artigo encontrado para o autor." });
            
            return Ok(articlesFound);
        }

        private void MockData()
        {
            this.articles = new List<Article>();
            articles.Add(new Article
            {
                IdArticle = 1,
                DatePublic = new DateTime(2022,2,15).ToString("dd/MM/yyyy"),
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
                DatePublic = new DateTime(2022,3,10).ToString("dd/MM/yyyy"),
                TextArticle = @"A tecnologia foi a grande aliada da humanidade no combate ao Covid-19. Seja no rápido desenvolvimento das vacinas, na velocidade do compartilhamento de informações, ou mesmo pela possibilidade do trabalho remoto. Neste artigo, vamos contar como o governo britânico utilizou a plataforma Azure no combate à pandemia.
                
                                Atuando desde 1948, o Serviço Nacional de Saúde britânico (NHS – National Health Service) nunca havia enfrentado uma ameaça de saúde tão inesperada quanto a pandemia do Covid-19.

                                Para responder à gigante demanda por serviços de saúde, ficou claro que a NHS precisava de uma arquitetura de sistemas que pudesse crescer o suficiente para lidar com o tsunami de solicitações, mantendo performance adequada.

                                A grande demanda de acesso ao site 111 online, principal centro de informações sobre COVID-19, resultou em problemas de desempenho e interrupções graves no sistema. Por isso, o governo britânico utilizou a plataforma Azure para escalar sua infraestrutura de acesso a informações no combate à pandemia. Mais especificamente, adotando Estrutura Bem Projetada do Microsoft Azure .",
                IdAuthor = 1,
                Title = "Governo britânico utilizou Azure no combate à pandemia",
                Tags = new List<string> { "cloud", "azure", "pandemia","covid","escalabilidade" },
                Summary = @"Manter a performance adequada para lidar com o tsunami de solicitações no combate à pandemia de Covid-19. Saiba como o Serviço Nacional de Saúde Britânico utilizou as soluções Microsoft Azure para escalar sua infraestrutura de acesso a informações.",
                UrlBanner = "https://source.unsplash.com/Fa9b57hffnM/800x300"
            });

            articles.Add(new Article
            {
                IdArticle = 3,
                DatePublic = new DateTime(2022,1,4).ToString("dd/MM/yyyy"),
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
                IdArticle = 4,
                DatePublic = new DateTime(2022,2,12).ToString("dd/MM/yyyy"),
                TextArticle = @"É até difícil de acreditar, mas já se passaram dois anos da “Era COVID” e o isolamento social impulsionou as empresas para o digital. Tanto pelo trabalho remoto quanto pela necessidade de comprar sem sair de casa. O varejo mudou, e quando o varejo se transforma, toda cadeia que o suporta, também.

                                Tarefas do dia-a-dia, como mercado, farmácia, e até roupas e acessórios praticamente migraram do presencial para o remoto. Desde a busca e esclarecimentos, até a compra e a devolução. Tudo passou a acontecer com força no meio digital. Tanto para as grandes marcas, quanto para os pequenos varejistas. 
                                
                                Uma tendência crescente antes mesmo do início da pandemia, a Realidade Virtual permite a imersão em ambientes virtuais como jogos, seminários virtuais e pontos turísticos. Pode ser feito por computador ou com equipamentos especiais como óculos e capacetes de imersão. As iniciativas mais conhecidas são usadas em jogos que simulam corridas, parques de diversão e aventuras onde o usuário assume o lugar do personagem principal e comanda suas ações.

                                Com o lançamento do Metaverso, o Facebook (agora Meta) propõe um novo conceito de interação humana e apresenta um universo totalmente compartilhado entre real e virtual. Iniciativas como o Mesh da Microsoft, também entram na briga por uma fatia desse novo mercado. É o mundo real se aproximando dos filmes futuristas de ficção científica.",
                IdAuthor = 2,
                Title = "Tendências tecnológicas para 2022",
                Tags = new List<string> { "tendências", "2022", "mercado", "tecnologia","virtual" },
                Summary = @"Após dois anos de pandemia é perceptível que a forma como consumimos mudou. Por isso, o Digital First será a o guia para 2022. Entenda esta e outras tendências.",
                UrlBanner = "https://source.unsplash.com/Dz5j0QKVUGY/800x300"
            });

            articles.Add(new Article
            {
                IdArticle = 5,
                DatePublic = new DateTime(2022,4,8).ToString("dd/MM/yyyy"),
                TextArticle = @"Um dos projetos mais cobiçados pelos amantes da tecnologia é a automação residencial Arduino, onde você pode, por meio de aplicativos, comandar a casa como um todo, permitindo abrir e fechar cortinas e janelas motorizadas, ligar e desligar televisores em horários pré-definidos, comandar ventiladores e tudo mais que você pensar, isso diretamente em seu telefone, tablet ou computador.
                                
                                Grandes coisas em automação podem serem feitas adicionando ao Arduino um Ethernet Shield, permitindo a você transformar o pequeno microcontrolador em um dispositivo conectado à Internet, capaz de mudar o estado de luzes, TVs, máquinas de café – praticamente qualquer coisa que você pode pensar – ligado e desligado, utilizando uma interface baseada em browser ou um temporizador.",
                IdAuthor = 3,
                Title = "Cadeiras automatizadas com arduino",
                Tags = new List<string> { "Arduino", "Cadeiras", "Programação","hardware" },
                Summary = @"A automatização hoje em dia é possível através de várias tecnologias, entre elas o arduino, no artigo abordaremos como automatizar a regulagem de uma cadeira de escritório.",
                UrlBanner = "https://source.unsplash.com/zP7X_B86xOg/800x300"
            });

            articles.Add(new Article
            {
                IdArticle = 6,
                DatePublic = new DateTime(2022,4,21).ToString("dd/MM/yyyy"),
                TextArticle = @"O ataque normalmente é percebido pela equipe de monitoramento – NOC ou SOC. Quando detecta um ataque, a empresa deve acionar sua equipe de Segurança da Informação que irá conduzir as ações de mitigação e comunicação necessárias.
                                
                                A primeira ação da equipe de Segurança será mitigar o ataque. Por vezes, isto significa derrubar sistemas, interromper processamento, chavear todas as portas externas e internas. Ataques Ransomware se propagam de dentro pra fora. É muito comum que o vírus infecte o ambiente muito antes de, de fato, iniciar o ataque. Neste tempo, os criminosos mapeam a arquitetura da infraestrutura de TI da empresa e aprendem os caminhos aos sistemas mais importantes. Só então realizam o ataque, “sequestrando”os dados através de criptografia.
                                
                                Algumas variações desses ransomware podem ser removidas com antivírus sem grandes problemas. Porém a melhor forma de garantir que os seus dados estarão protegidos é manter o backup dos seus arquivos atualizados.",
                IdAuthor = 3,
                Title = "Proteja sua empresa de ataques ransomware",
                Tags = new List<string> { "segurança", "ransomware", "ataques" },
                Summary = @"O número de ataques cibernéticos tem aumentado muito rapidamente nas empresas nos últimos meses. Nesse artigo você entenderá os principais pontos de atenção para proteger sua empresa de ataques de ransomware.",
                UrlBanner = "https://source.unsplash.com/w7ZyuGYNpRQ/800x300"
            });

            articles.Add(new Article
            {
                IdArticle = 7,
                DatePublic = new DateTime(2022,4,18).ToString("dd/MM/yyyy"),
                TextArticle = @"NFT, sigla para Non-fungible Token (Token Não-Fungível), é uma chave eletrônica criptografada única, uma espécie de certificado de propriedade intelectual digital, que garante sua originalidade e autenticidade. Ou seja, é um item exclusivo, único e para o qual não existe nenhum equivalente.

                                Para entender a diferença entre “fungível” e “não-fungível”, podemos pensar nos seguintes exemplos: uma nota de R$100 pode ser trocada por outra de R$100, pois ambas são equivalentes apesar de serem dois objetos físicos distintos. Este é um exemplo um bem fungível.

                                Já o quadro Monalisa de DaVinci não é equivalente a nenhuma outra pintura e tem um valor próprio. Não existe nenhum equivalente, apenas cópias. Este é um exemplo de bem infungível, algo único e específico.

                                Outro exemplo é o quadro Nascimento de Vênus, de Sandro Botticelli. Todos já vimos várias reproduções dessa obra em diversos meios e formatos. Porém, essas são somente cópias sem valor físico ou sentimental. A obra original se encontra na Galleria degli Uffizi em Florença, Itália, e por mais que todas essas cópias tentem se aproximar da pintura original os seus valores comerciais não chegam nem próximo da obra-prima de Botticelli. O valor inestimável da peça é o mesmo conceito artístico que o NFT toma emprestado e traz para o mundo virtual.",
                IdAuthor = 3,
                Title = "Entenda o que são NFTs e como funciona a tecnologia dos Tokens Não-Fungíveis",
                Tags = new List<string> { "nft", "tokens", "cripto","moeda","token" },
                Summary = @"O número de ataques cibernéticos tem aumentado muito rapidamente nas empresas nos últimos meses. Nesse artigo você entenderá os principais pontos de atenção para proteger sua empresa de ataques de ransomware.",
                UrlBanner = "https://source.unsplash.com/WN5_7UBc7cw/800x300"
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
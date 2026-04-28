using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questrix.Application.Features.Surveys.Commands.Add;
using Questrix.Application.Models;

namespace Questrix.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveysController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Add(CancellationToken cancellationToken) => Ok(await mediator.Send(new AddSurveyCommandRequest
        {
            Title = "Sosyal Psikolojide Okuma ve Araştırma Dersi Kapsamında Bir Çalışma",
            Description = "Bu çalışma, PSYC416: Reading and Research in Social Psychology dersi kapsamında geliştirilen bir araştırma tasarımı için hazırlanmıştır. \r\n\r\nAnketin yaklaşık 15 dakika sürmesi beklenmektedir. Katılım tamamen gönüllüdür. İstediğiniz zaman ankete son verebilir veya herhangi bir soruyu yanıtlamamayı tercih edebilirsiniz. Yanıtlar yalnızca ders kapsamındaki akademik amaçlarla değerlendirilecektir.\r\n\r\nBu ankette kimliğinizi doğrudan belirleyecek kişisel bilgiler istenmemektedir. Yanıtlarınız gizli tutulacaktır ve bireysel düzeyde üçüncü kişilerle paylaşılmayacaktır.\r\n\r\nAşağıdaki kutuyu işaretlemeniz, araştırma hakkında yeterli bilgi aldığınızı ve gönüllü olarak katılmayı kabul ettiğinizi gösterir.",
            Nodes = [
                new() {
                    Id = Guid.Parse("758e290b-c83d-4358-8556-7f7de3f84fb4"),
                    Type = Domain.Entities.SurveyNodeType.MultipleChoice,
                    Question = "Cinsiyetiniz *",
                    Options = [
                        new() {
                            Label = "Kadın",
                            Value = "0"
                        },
                        new() {
                            Label = "Erkek",
                            Value = "1"
                        },
                        new() {
                            Label = "Belirtmek istemiyorum",
                            Value = "2"
                        }
                        ],
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("8efcc2da-a88d-44a9-90ec-e5365de2a7cf"),
                            IsDefault = true,
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("8efcc2da-a88d-44a9-90ec-e5365de2a7cf"),
                    Type = Domain.Entities.SurveyNodeType.ShortAnswer,
                    Question = "Yaşınız *",
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("baa00d8e-fec2-44f8-b6c7-3907caac91d2"),
                            IsDefault = true,
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("baa00d8e-fec2-44f8-b6c7-3907caac91d2"),
                    Type = Domain.Entities.SurveyNodeType.MultipleChoice,
                    Question = "Eğitim seviyeniz *",
                    Options = [
                        new() {
                            Label = "İlkokul",
                            Value = "0"
                        },
                        new() {
                            Label = "Ortaokul",
                            Value = "1"
                        },
                        new() {
                            Label = "Lise",
                            Value = "2"
                        },
                        new() {
                            Label = "Önlisans",
                            Value = "3"
                        },
                        new() {
                            Label = "Lisans",
                            Value = "4"
                        },
                        new() {
                            Label = "Yüksek lisans",
                            Value = "5"
                        },
                        new() {
                            Label = "Doktora",
                            Value = "6"
                        },
                        new() {
                            Label = "Diğer",
                            Value = "7"
                        }
                        ],
                    Rules = [
                        new() {
                            Condition = "answer == 'Diğer'",
                            NextNodeId = Guid.Parse("33db48f6-58e2-4d90-a32d-c3d5ef4c7b70"),
                        },
                        new() {
                            NextNodeId = Guid.Parse("5a04b543-ccdd-41d1-9a5e-25d41ad2544d"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("33db48f6-58e2-4d90-a32d-c3d5ef4c7b70"),
                    Type = Domain.Entities.SurveyNodeType.ShortAnswer,
                    Question = "Diğer",
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("5a04b543-ccdd-41d1-9a5e-25d41ad2544d"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("5a04b543-ccdd-41d1-9a5e-25d41ad2544d"),
                    Type = Domain.Entities.SurveyNodeType.MultipleChoice,
                    Question = "Politik görüşünüz aşağıdaki kategorilerden hangisine daha yakındır? (lütfen sadece en yakın olduğunuz “bir” seçeneği işaretleyin) *",
                    Options = [
                        new() {
                            Label = "Milliyetçi",
                            Value = "0"
                        },
                        new() {
                            Label = "Sosyalist",
                            Value = "1"
                        },
                        new() {
                            Label = "Sosyal Demokrat",
                            Value = "2"
                        },
                        new() {
                            Label = "Muhafazakar",
                            Value = "3"
                        },
                        new() {
                            Label = "Apolitik",
                            Value = "4"
                        },
                        new() {
                            Label = "Diğer",
                            Value = "5"
                        }
                        ],
                    Rules = [
                        new() {
                            Condition = "answer == 'Diğer'",
                            NextNodeId = Guid.Parse("845f13d2-beff-4f64-b565-19d03500a21c"),
                        },
                        new() {
                            NextNodeId = Guid.Parse("5619bac3-2d14-4d94-ae50-29ecf99d9389"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("845f13d2-beff-4f64-b565-19d03500a21c"),
                    Type = Domain.Entities.SurveyNodeType.ShortAnswer,
                    Question = "Diğer",
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("5619bac3-2d14-4d94-ae50-29ecf99d9389"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("5619bac3-2d14-4d94-ae50-29ecf99d9389"),
                    Type = Domain.Entities.SurveyNodeType.LinearScale,
                    Question = "5) Politik görüşünüzü nerede konumlandırırsınız? *",
                    Metadata = JsonConvert.SerializeObject(new LinearScaleMetadata {
                        Min = 1,
                        Max = 7,
                        MinLabel = "Aşırı sol",
                        MaxLabel = "Aşırı sağ"
                    }),
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("c0c9ed42-1fdb-498a-bd33-bc143bf1521d"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("c0c9ed42-1fdb-498a-bd33-bc143bf1521d"),
                    Type = Domain.Entities.SurveyNodeType.MultipleChoice,
                    Question = "Şu anki gelir seviyenizi tanımlar mısınız? *",
                    Options = [
                        new() {
                            Label = "Çok düşük",
                            Value = "0"
                        },
                        new() {
                            Label = "Düşük",
                            Value = "1"
                        },
                        new() {
                            Label = "Orta",
                            Value = "2"
                        },
                        new() {
                            Label = "Yüksek",
                            Value = "3"
                        },
                        new() {
                            Label = "Çok yüksek",
                            Value = "4"
                        }
                        ],
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("812dfecf-4927-4b86-ba6f-bf7227a27663"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("812dfecf-4927-4b86-ba6f-bf7227a27663"),
                    Type = Domain.Entities.SurveyNodeType.MultipleChoice,
                    Question = "Aşağıdakilerden hangisi sizin dini/inanç sisteminizi en iyi ifade etmektedir? *",
                    Options = [
                        new() {
                            Label = "Tanrı’ya inanmam (Ateistim)",
                            Value = "0"
                        },
                        new() {
                            Label = "Tanrı’ya inanıyor ama bir dini tercih etmiyorum",
                            Value = "1"
                        },
                        new() {
                            Label = "Müslümanım",
                            Value = "2"
                        },
                        new() {
                            Label = "Diğer",
                            Value = "3"
                        }
                        ],
                    Rules = [
                        new() {
                            Condition = "answer == 'Diğer'",
                            NextNodeId = Guid.Parse("726381b6-e56c-41ed-801c-302d3a893832"),
                        },
                        new() {
                            NextNodeId = Guid.Parse("13f8eb0a-9c31-46b4-945a-6e3e3c12bc56"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("726381b6-e56c-41ed-801c-302d3a893832"),
                    Type = Domain.Entities.SurveyNodeType.ShortAnswer,
                    Question = "Diğer",
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("13f8eb0a-9c31-46b4-945a-6e3e3c12bc56"),
                            IsDefault = true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("13f8eb0a-9c31-46b4-945a-6e3e3c12bc56"),
                    Type = Domain.Entities.SurveyNodeType.LinearScale,
                    Question = "Kendinizi dindar/inanan biri olarak nitelendirir misiniz? *",
                    Metadata = JsonConvert.SerializeObject(new LinearScaleMetadata() {
                        Min = 1,
                        Max = 7,
                        MinLabel = "Hiç dindar değilim",
                        MaxLabel = "Evet çok dindarım"
                    }),
                    Rules = [
                        new() {
                            NextNodeId = Guid.Parse("6c10d598-94da-46f3-a645-349fca77040b"),
                            IsDefault= true
                        }
                        ]
                },
                new() {
                    Id = Guid.Parse("6c10d598-94da-46f3-a645-349fca77040b"),
                    Type = Domain.Entities.SurveyNodeType.MultipleChoice,
                    Question = "En uzun süreyle yaşadığınız yer: *",
                    Options = [
                        new() {
                            Label = "Büyükşehir",
                            Value = "0"
                        },
                        new() {
                            Label = "Şehir",
                            Value = "1"
                        },
                        new() {
                            Label = "Kasaba",
                            Value = "2"
                        },
                        new() {
                            Label = "Belde",
                            Value = "3"
                        },
                        new() {
                            Label = "Köy",
                            Value = "4"
                        }
                        ]
                }
                ],
            InvitationCodeMaxUsege = 100,
            InvitationCodeExpiresAt = DateTime.UtcNow.AddDays(7)
        }, cancellationToken));
    }
}

// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace WalletMate.Infrastructure.Tests.Scenarios
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class PeriodFeature : Xunit.IClassFixture<PeriodFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Period.feature"
#line hidden
        
        public PeriodFeature(PeriodFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Period", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line 7
 testRunner.Given("Je suis connecté à l\'application avec l\'utilisateur aurelien et le mot de passe 0" +
                    "f46f2fb6f5a91c79e86acc5da7df95176b4e4c7", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Création d\'une nouvelle période")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Création d\'une nouvelle période")]
        public virtual void CreationDuneNouvellePeriode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Création d\'une nouvelle période", null, ((string[])(null)));
#line 9
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 10
 testRunner.When("Je demande la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("La liste des périodes contient \"Janvier 2001\" avec l\'identifiant 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Intérdire la création de deux mêmes périodes")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Intérdire la création de deux mêmes périodes")]
        public virtual void InterdireLaCreationDeDeuxMemesPeriodes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Intérdire la création de deux mêmes périodes", null, ((string[])(null)));
#line 13
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 14
 testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.When("Je demande la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.Then("Le serveur me retourne une erreur 400 avec le message \"La période Janvier 2001 ex" +
                    "iste déjà.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Ajouter une dépense à une période")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Ajouter une dépense à une période")]
        public virtual void AjouterUneDepenseAUnePeriode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Ajouter une dépense à une période", null, ((string[])(null)));
#line 18
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 19
 testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table1.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Aurélien",
                        "Commun"});
#line 20
 testRunner.When("J\'ajoute des dépenses dans l\'application", ((string)(null)), table1, "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "operationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table2.AddRow(new string[] {
                        "Dépense",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Aurélien",
                        "Commun"});
#line 23
 testRunner.Then("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Ajouter une recette à une période")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Ajouter une recette à une période")]
        public virtual void AjouterUneRecetteAUnePeriode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Ajouter une recette à une période", null, ((string[])(null)));
#line 27
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 28
 testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table3.AddRow(new string[] {
                        "2001-01",
                        "200",
                        "Test de recette",
                        "Marie",
                        "Commun"});
#line 29
 testRunner.When("J\'ajoute des recettes dans l\'application", ((string)(null)), table3, "When ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table4.AddRow(new string[] {
                        "Recette",
                        "1",
                        "2001-01",
                        "200",
                        "Test de recette",
                        "Marie",
                        "Commun"});
#line 32
 testRunner.Then("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table4, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Supprimer une dépense d\'une période")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Supprimer une dépense d\'une période")]
        public virtual void SupprimerUneDepenseDunePeriode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Supprimer une dépense d\'une période", null, ((string[])(null)));
#line 36
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 37
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table5.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Aurélien",
                        "Commun"});
#line 38
 testRunner.And("J\'ai ajouté des dépenses dans l\'application", ((string)(null)), table5, "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table6.AddRow(new string[] {
                        "Dépense",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Aurélien",
                        "Commun"});
#line 41
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table6, "And ");
#line 44
 testRunner.When("Je demande à supprimer l\'opération 1 de la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.Then("La liste des opérations pour la période 2001-01 est vide", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Supprimer une recette d\'une période")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Supprimer une recette d\'une période")]
        public virtual void SupprimerUneRecetteDunePeriode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Supprimer une recette d\'une période", null, ((string[])(null)));
#line 47
 this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 48
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table7.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Aurélien",
                        "Commun"});
#line 49
 testRunner.And("J\'ai ajouté des recettes dans l\'application", ((string)(null)), table7, "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table8.AddRow(new string[] {
                        "Recette",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Aurélien",
                        "Commun"});
#line 52
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table8, "And ");
#line 55
 testRunner.When("Je demande à supprimer l\'opération 1 de la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 56
 testRunner.Then("La liste des opérations pour la période 2001-01 est vide", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le montant d\'une opération de recette")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le montant d\'une opération de recette")]
        public virtual void ModifierLeMontantDuneOperationDeRecette()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le montant d\'une opération de recette", null, ((string[])(null)));
#line 58
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 59
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table9.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 60
 testRunner.And("J\'ai ajouté des recettes dans l\'application", ((string)(null)), table9, "And ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table10.AddRow(new string[] {
                        "Recette",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 63
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table10, "And ");
#line 66
 testRunner.When("je demande à modifier le montant de la recette numéro 1 en 200 euros pour la péri" +
                    "ode 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
 testRunner.Then("L\'opération 1 de la période 2001-01 à un montant de 200 euros", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 68
 testRunner.And("Marie doit la somme de 100 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le libellé d\'une opération de recette")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le libellé d\'une opération de recette")]
        public virtual void ModifierLeLibelleDuneOperationDeRecette()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le libellé d\'une opération de recette", null, ((string[])(null)));
#line 70
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 71
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table11.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 72
 testRunner.And("J\'ai ajouté des recettes dans l\'application", ((string)(null)), table11, "And ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table12.AddRow(new string[] {
                        "Recette",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 75
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table12, "And ");
#line 78
 testRunner.When("je demande à modifier le libellé de la recette numéro 1 en \"essai\" pour la périod" +
                    "e 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 79
 testRunner.Then("L\'opération 1 de la période 2001-01 à pour libellé \"essai\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
 testRunner.And("Marie doit la somme de 50 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le binôme d\'une opération de recette")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le binôme d\'une opération de recette")]
        public virtual void ModifierLeBinomeDuneOperationDeRecette()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le binôme d\'une opération de recette", null, ((string[])(null)));
#line 82
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 83
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table13.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 84
 testRunner.And("J\'ai ajouté des recettes dans l\'application", ((string)(null)), table13, "And ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table14.AddRow(new string[] {
                        "Recette",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 87
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table14, "And ");
#line 90
 testRunner.When("je demande à modifier le binôme de la recette numéro 1 par Aurélien pour la pério" +
                    "de 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
 testRunner.Then("L\'opération 1 de la période 2001-01 à pour binôme Aurélien", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 92
 testRunner.And("Aurélien doit la somme de 50 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le type d\'une opération de recette")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le type d\'une opération de recette")]
        public virtual void ModifierLeTypeDuneOperationDeRecette()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le type d\'une opération de recette", null, ((string[])(null)));
#line 94
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 95
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table15.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 96
 testRunner.And("J\'ai ajouté des recettes dans l\'application", ((string)(null)), table15, "And ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table16.AddRow(new string[] {
                        "Recette",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 99
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table16, "And ");
#line 102
 testRunner.When("je demande à modifier le type de la recette numéro 1 en \"Individuelle\" pour la pé" +
                    "riode 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 103
 testRunner.Then("L\'opération 1 de la période 2001-01 à pour type \"Individuelle\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 104
 testRunner.And("Marie doit la somme de 100 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le montant d\'une opération de dépense")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le montant d\'une opération de dépense")]
        public virtual void ModifierLeMontantDuneOperationDeDepense()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le montant d\'une opération de dépense", null, ((string[])(null)));
#line 106
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 107
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table17.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 108
 testRunner.And("J\'ai ajouté des dépenses dans l\'application", ((string)(null)), table17, "And ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table18.AddRow(new string[] {
                        "Dépense",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 111
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table18, "And ");
#line 114
 testRunner.When("je demande à modifier le montant de la dépense numéro 1 en 200 euros pour la péri" +
                    "ode 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 115
 testRunner.Then("L\'opération 1 de la période 2001-01 à un montant de 200 euros", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 116
 testRunner.And("Aurélien doit la somme de 100 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le libellé d\'une opération de dépense")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le libellé d\'une opération de dépense")]
        public virtual void ModifierLeLibelleDuneOperationDeDepense()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le libellé d\'une opération de dépense", null, ((string[])(null)));
#line 118
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 119
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table19.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 120
 testRunner.And("J\'ai ajouté des dépenses dans l\'application", ((string)(null)), table19, "And ");
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table20.AddRow(new string[] {
                        "Dépense",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 123
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table20, "And ");
#line 126
 testRunner.When("je demande à modifier le libellé de la dépense numéro 1 en \"essai\" pour la périod" +
                    "e 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 127
 testRunner.Then("L\'opération 1 de la période 2001-01 à pour libellé \"essai\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 128
 testRunner.And("Aurélien doit la somme de 50 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le binôme d\'une opération de dépense")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le binôme d\'une opération de dépense")]
        public virtual void ModifierLeBinomeDuneOperationDeDepense()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le binôme d\'une opération de dépense", null, ((string[])(null)));
#line 130
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 131
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table21.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 132
 testRunner.And("J\'ai ajouté des dépenses dans l\'application", ((string)(null)), table21, "And ");
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table22.AddRow(new string[] {
                        "Dépense",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 135
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table22, "And ");
#line 138
 testRunner.When("je demande à modifier le binôme de la dépense numéro 1 par Aurélien pour la pério" +
                    "de 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 139
 testRunner.Then("L\'opération 1 de la période 2001-01 à pour binôme Aurélien", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 140
 testRunner.And("Marie doit la somme de 50 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Modifier le type d\'une opération de dépense")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Modifier le type d\'une opération de dépense")]
        public virtual void ModifierLeTypeDuneOperationDeDepense()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modifier le type d\'une opération de dépense", null, ((string[])(null)));
#line 142
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 143
testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table23.AddRow(new string[] {
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 144
 testRunner.And("J\'ai ajouté des dépenses dans l\'application", ((string)(null)), table23, "And ");
#line hidden
            TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "OperationId",
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table24.AddRow(new string[] {
                        "Dépense",
                        "1",
                        "2001-01",
                        "100",
                        "Test",
                        "Marie",
                        "Commun"});
#line 147
 testRunner.And("La liste des opérations pour la période 2001-01 contient les elements suivants", ((string)(null)), table24, "And ");
#line 150
 testRunner.When("je demande à modifier le type de la dépense numéro 1 en \"Avance\" pour la période " +
                    "2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 151
 testRunner.Then("L\'opération 1 de la période 2001-01 à pour type \"Avance\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 152
 testRunner.And("Aurélien doit la somme de 100 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Obtenir le solde d\'une période après de multiples opérations")]
        [Xunit.TraitAttribute("FeatureTitle", "Period")]
        [Xunit.TraitAttribute("Description", "Obtenir le solde d\'une période après de multiples opérations")]
        public virtual void ObtenirLeSoldeDunePeriodeApresDeMultiplesOperations()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Obtenir le solde d\'une période après de multiples opérations", null, ((string[])(null)));
#line 154
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 155
 testRunner.And("J\'ai demandé la création d\'une période pour le mois 1 et l\'année 2001", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table25.AddRow(new string[] {
                        "2001-01",
                        "150",
                        "leclerc",
                        "Marie",
                        "Commun"});
            table25.AddRow(new string[] {
                        "2001-01",
                        "200",
                        "cadeau",
                        "Aurelien",
                        "Commun"});
            table25.AddRow(new string[] {
                        "2001-01",
                        "55",
                        "edf",
                        "Aurelien",
                        "Commun"});
            table25.AddRow(new string[] {
                        "2001-01",
                        "30",
                        "docteur",
                        "Marie",
                        "Avance"});
#line 156
 testRunner.When("J\'ajoute des dépenses dans l\'application", ((string)(null)), table25, "When ");
#line hidden
            TechTalk.SpecFlow.Table table26 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periode",
                        "Montant",
                        "Libelle",
                        "Binome",
                        "Categorie"});
            table26.AddRow(new string[] {
                        "2001-01",
                        "200",
                        "CAF",
                        "Marie",
                        "Commun"});
#line 162
 testRunner.And("J\'ajoute des recettes dans l\'application", ((string)(null)), table26, "And ");
#line 165
 testRunner.Then("Marie doit la somme de 122.5 euros pour la période 2001-01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                PeriodFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                PeriodFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class TerminalBehaviour : MonoBehaviour
{
    const string menuHint = "Puoi scrivere nel menu quando vuoi";
    enum Screen { MainMenu, Password, Win};
    Screen currentScreen;
    string currentName = "Suusan";
    // Start is called before the first frame update


    private void Awake()
    {
        instance = this;
    }
    void Start()

    {
        ShowMainMenu();
    }

    void OnUserInput(string input)
    {
        switch (input.Normalize().ToLower())
        {
            case "niente":
            case "no":
            case "nulla":
                ShowEnd();
                break;

            case "sudo su":
                currentName = "root";
                ShowMainMenu();
                break;
            case "ricomincia":
            case "voglio ricominciare":
            case "restart":
                ShowRestart();
                break;
            
            case "menu":
                ShowMainMenu();
                break;
            case "3d":
            case "2d":
            case "pixel":
                ///CAMBIA SCENA
                break;
            default:
                //SendRequest(input);
                ShowEnd();
                break;
        }
    }
    void ShowMainMenu()
    {
       // SendRequest();
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        StartCoroutine(IFirstSentence());

    }

    void ShowEnd()
    {
        Terminal.ClearScreen();
        currentScreen = Screen.MainMenu;
        StartCoroutine(IEnd());

    }

    void ShowRestart()
    {
        
        Terminal.ClearScreen();
        StartCoroutine(IRestartSentence());

    }


    private IEnumerator IFirstSentence()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(WriteStringInTerminal($"Ben Arrivata {currentName}"));
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(WriteStringInTerminal($"Prima di vincere la partita vuoi scrivere qualcosa?"));
    }

    private IEnumerator IRestartSentence()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(WriteStringInTerminal($"Bene {currentName}, la scelta è tua, Tavvauvutit!"));
        /////RITORNO ALL'INIZIO
    }

    private IEnumerator IChatResponse(string test)
    {
       
        yield return StartCoroutine(WriteStringInTerminal(test));
        yield return new WaitForSeconds(1.5f);
        ShowMainMenu();
        /////RITORNO ALL'INIZIO
    }

    private IEnumerator IEnd()
    {
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(WriteStringInTerminal($"Bene mia cara {currentName}, hai vinto la partita! Tavvauvutit!"));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(WriteStringInTerminal("Ora che sai la storia, dove vorresti finire? [3d,2d,pixel] "));
        


    }

    public IEnumerator WriteStringInTerminal(string content)
    {
        for(int i = 1; i <= content.Length; i++)
        {
            Terminal.ClearScreen();
            Terminal.WriteLine(content.Substring(0, i));
            yield return new WaitForSeconds(.1f);
        }
    }

    private  void SendRequest(string request)

    {
        var chat = new OpenAI.ChatGPT2();
        chat.SendReply(request);
        //Debug.Log(response);
    }
    public static TerminalBehaviour instance;
    public void WriteChat(string test)
    {
        Terminal.ClearScreen();
        StartCoroutine(IChatResponse(test));
        


    }



}
namespace OpenAI
{
    public class ChatGPT2 
    {


        private OpenAIApi openai = new OpenAIApi();

        private string response;
        // private string Instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";


      
        public async void SendReply(string request)
        {
            //userInput = inputField.text;




            Debug.Log(request);

            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = request,
                Model = "text-davinci-003",
                MaxTokens = 128
            });
            Debug.Log(completionResponse.Choices[0].Text);
            TerminalBehaviour.instance.WriteChat(completionResponse.Choices[0].Text);
           // return  completionResponse.Choices[0].Text;

        }
    }
}
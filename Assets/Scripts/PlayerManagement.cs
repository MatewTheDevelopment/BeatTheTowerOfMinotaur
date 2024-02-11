using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] private GameObject playerTail;

    [SerializeField] private AudioSource winSource;

    [SerializeField] private Animation fade;

    [SerializeField] private AnimationClip open, close;

    [SerializeField] private LayerMask solid, useful;

    [SerializeField] private Text moneyCount;

    [SerializeField] private int currentMoney, maxMoney;

    [SerializeField] private float distance, openOffset, closeOffset;

    private bool endOfTurn;

    private Vector3 targetPoint;

    private void Awake()
    {
        moneyCount.text = "Money: " + currentMoney + "/" + maxMoney;

        Invoke("OpenOffset", openOffset);
    }

    private void Update()
    {
        InputCheck();
    }

    private void InputCheck()
    {
        if (endOfTurn == false && (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0) || (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") != 0))
        {
            targetPoint = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 2;

            for (bool endOfState = false; endOfState != true;)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, targetPoint, out hit, distance, solid))
                {
                    break;
                }
                else
                {
                    var currentTail = Instantiate(playerTail, transform.position, transform.rotation);

                    Destroy(currentTail, 0.15f);

                    transform.Translate(targetPoint);

                    Collider[] collection = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), transform.rotation, useful);

                    for (int i = 0; i < collection.Length; i++)
                    {
                        collection[i].gameObject.GetComponent<MoneyManagement>().Execute();

                        currentMoney++;

                        moneyCount.text = "Money: " + currentMoney + "/" + maxMoney;

                        moneyCount.gameObject.GetComponent<Animation>().Play();

                        if(currentMoney >= maxMoney)
                        {
                            winSource.Play();

                            gameObject.GetComponent<Animation>().Play();

                            Invoke("CloseOffset", closeOffset);

                            endOfTurn = true;
                        }
                    }
                }
            }
        }

        else if (Input.GetKey(KeyCode.Escape))
        {
            fade.Play(close.name);

            Invoke("BackToMenu", fade.GetClip(close.name).length);
        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OpenOffset()
    {
        fade.Play(open.name);
    }

    private void CloseOffset()
    {
        fade.Play(close.name);

        Invoke("LoadScene", fade.GetClip(close.name).length);
    }

    private void LoadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if(index >= 6)
        {
            index = 0;
        }

        SceneManager.LoadScene(index);

        Debug.Log(index);
    }
}

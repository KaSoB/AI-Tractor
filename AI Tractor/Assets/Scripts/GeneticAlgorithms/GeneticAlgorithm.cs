using System.Collections;
using System.Text;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GeneticAlgorithm {
    int[,] values = new int[13, 10];
    int[,] values2 = new int[13, 10];
    float[] fieldValue = new float[13];
    float[] distribution = new float[13];
    int[] maxGen = new int[13];
    float maxGenValue = 0;

    public void setEmptyGen(){
        for (int i = 0; i < 13; i++){
            maxGen[i] = 0;
        }
    }


    public void init() {
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 13; j++) {
                //Random rnd = new Random();
                int binom = UnityEngine.Random.Range(0, 2);
                values[j, i] = binom;
            }
        }
    }

    public void cross(int w,int t) {
        int k = Random.Range(1, 12);
       // Debug.Log("k"+k);
        for (int i = k; i < 13; i++) {
            int c = values2[i, w];
            values2[i, w] = values2[i, t];
            values2[i, t] = c;
        }
    }

    public void mutation(double probability){
        //Random rand = new Random();
        int mutation = 0;
        for (int i = 0; i < 10; i++){
            for (int j = 0; j < 13; j++){
                if (Random.value < probability){
                    values2[j, i] = 1 - values[j, i];
                    mutation = mutation + 1;
                }
            }
        }
       // Debug.Log("Liczba mutacji = " + mutation);
    }

    public void rewriteValuesToValues2() {
        for (int j = 0; j < 13; j++)
            for (int i = 0; i < 10; i++)
                values2[j, i] = values[j, i];
    }
    
    public void findValueOfAllFarmField(){
        FarmField[] farmFields = GameObject.FindObjectsOfType(typeof(FarmField)) as FarmField[];
        int i = 0;
        foreach (var farmField in farmFields){
            if (farmField.ToString().Equals("Corn field")){
                fieldValue[i] = 10*farmField.Progress+10 * farmField.GetLevel(Property.Type.Fertylity) + farmField.GetLevel(Property.Type.Humidity) -
                    8 * farmField.GetLevel(Property.Type.Pollution) - 2 * farmField.GetLevel(Property.Type.Acidity);
            }
            if (farmField.ToString().Equals("Wheat field"))
            {
                fieldValue[i] = 10*farmField.Progress+ 9 * farmField.GetLevel(Property.Type.Fertylity) + 2*farmField.GetLevel(Property.Type.Humidity) -
                    5 * farmField.GetLevel(Property.Type.Pollution) - 3 * farmField.GetLevel(Property.Type.Acidity);
            }
            if (farmField.ToString().Equals("Carrot field"))
            {
                fieldValue[i] = 10* farmField.Progress+10 * farmField.GetLevel(Property.Type.Fertylity) + 2*farmField.GetLevel(Property.Type.Humidity) -
                    7 * farmField.GetLevel(Property.Type.Pollution) - 3 * farmField.GetLevel(Property.Type.Acidity);
            }

            //Debug.Log(farmField);
            /*
            Debug.Log("Progres: "+farmField.Progress);
            Debug.Log("Fertylity: "+farmField.GetLevel(Property.Type.Fertylity));
            Debug.Log(farmField.GetLevel(Property.Type.Humidity));
            Debug.Log(farmField.GetLevel(Property.Type.Pollution));
            Debug.Log(farmField.GetLevel(Property.Type.Acidity));
            */
            if (fieldValue[i] < 0)
                fieldValue[i] = 0;
            i = i + 1;
           // Debug.Log(i-1 +" = "+fieldValue[i - 1]);
        }
        
    }
    public void doDistribution(int a){
        float sum = 0;
        float vectorSum = 0;
        int usingFields=0;
        for (int i = 0; i < 10; i++){
            usingFields = 0;
            vectorSum = 0;
            for(int j=0; j < 13; j++){
                if (values2[j, i] == 1)  {
                    usingFields = usingFields + 1;
                    vectorSum = vectorSum + fieldValue[j];
                   }
                }
            sum = sum + vectorSum / (float)Mathf.Pow((float)1.5, (float)usingFields);

            if (vectorSum / (float)Mathf.Pow((float)1.5, (float)usingFields) > maxGenValue){
                maxGenValue = vectorSum / (float)Mathf.Pow((float)1.5,(float) usingFields);
                for (int j = 0; j < 13; j++)
                    maxGen[j] = values2[j, i];
                Debug.Log("Nowy max wartosc fenotypu to " + maxGenValue + "generacja: "+a);
            }
            distribution[i] = sum;
        }
        for (int i = 0; i < 10; i++)
            distribution[i] = distribution[i] * 100 / sum;
        /*
        Debug.Log("Dystrybuanty: "+distribution[0]);
        Debug.Log(distribution[1]);
        Debug.Log(distribution[2]);
        Debug.Log(distribution[3]);
        Debug.Log(distribution[4]);
        Debug.Log(distribution[5]);
        Debug.Log(distribution[6]);
        Debug.Log(distribution[7]);
        Debug.Log(distribution[8]);
        Debug.Log(distribution[9]);
        */
    }

    public void chooseGens(){
        double value;
        for (int i = 0; i < 10; i++){
            bool firstGreaterGen = false;
            for (int j = 0; j < 10; j++){
                value = 100*Random.value;
                if (value < distribution[j] && firstGreaterGen == false){
                    firstGreaterGen = true;
                    //Debug.Log("Wybrany gen: " + j + "Value: " + value);
                    for (int k = 0; k < 13; k++)
                    {
                        values[k, i] = values2[k, j];
                    }
                }
            }
        }
    }
    public List<FarmField> doAlgorithm(){
        init();
        setEmptyGen();
        findValueOfAllFarmField();
        for (int i = 0; i < 100; i++){
            rewriteValuesToValues2();
            for(int j = 0; j < 5; j++){
                cross(2 * j, 2 * j + 1);
            }
            if (i < 50)
                mutation(0.05);
            else
                mutation(0.02);
            doDistribution(i);
            chooseGens();

        }
        List <FarmField> farms = new List<FarmField>();
        FarmField[] farmFields = GameObject.FindObjectsOfType(typeof(FarmField)) as FarmField[];
        int p = 0;
        foreach (var farmField in farmFields){
            if (maxGen[p] == 1)
                farms.Add(farmField);
            p = p + 1;
         }

        return (farms);
    }
}    
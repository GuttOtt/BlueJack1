using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIconCSV : Singleton<CardIconCSV> {
	public static List<Dictionary<string, object>> data;

    protected override void Awake() {
        base.Awake();
		data = CSVReader.Read("CardIconCSV");
    }

    public static string GetNameByID(int id) {
		for (int i = 0; i < data.Count; i++) {
			if (Convert.ToInt32(data[i]["ID"]) == id) {
				return data[i]["Name"].ToString();
			}
		}
		return "ī�� �����Ͱ� �����ϴ�";
	}

	public static string GetDescByID(int id) {
		for (int i = 0; i < data.Count; i++) {
			if (Convert.ToInt32(data[i]["ID"]) == id) {
				return data[i]["Description"].ToString();
			}
		}
		return "ī�� �����Ͱ� �����ϴ�";
	}
}

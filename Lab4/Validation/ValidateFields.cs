﻿using System.Data;
using System.Windows;
using Lab4.Database;

namespace Lab4.Validation;

public static class ValidateFields
{
    public static async Task<bool> Isbn(string isbn, string initIsbn, AdoWrapper wrapper)
    {
        const string query = "SELECT isbn FROM Books";

        var table = await wrapper.ExecuteReader(query);
        
        foreach(DataRow row in table.Rows)
        {
            var tableIsbn = row["isbn"].ToString()!;

            if (isbn.Trim().Equals(initIsbn.Trim()))
            {
                continue;
            }

            if (isbn.Trim().Equals(tableIsbn.Trim()))
            {
                MessageBox.Show(messageBoxText: "There's already some ISBN like this",
                    caption: "Error!",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error,
                    defaultResult: MessageBoxResult.OK);
                
                return false;
            }
        }

        return true;
    }

    public static bool CastYear(string year)
    {
        var canBeCasted = int.TryParse(year, out _);

        if (!canBeCasted)
        {
            MessageBox.Show(messageBoxText: "Something wrong with year",
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
        }

        return canBeCasted;
    }
    
    public static bool IsEmpty(string field)
    {
        if (!field.Trim().Equals("")) return false;
        
        MessageBox.Show(messageBoxText: "Some field is empty.",
            caption: "Error!",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error,
            defaultResult: MessageBoxResult.OK);

        return true;
    }
}